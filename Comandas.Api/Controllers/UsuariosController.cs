using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comandas.Api.Data;
using Comandas.Api.Models;
using Comandas.Api.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Comandas.Api.Controllers
{
    [Tags("5. Usuarios")]
    [Route("api/[controller]")]
    [ApiController]

    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Realiza o Login do Usuário na Aplicação
        /// </summary>
        /// <param name="usuarioRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Realiza o Login do Usuário na Aplicação", Description = "Gera um Token de Autenticação JWT")]
        [SwaggerResponse(200,"Retorna um Token de Autenticação JWT",typeof(UsuarioResponse))]
        [SwaggerResponse(400, "BadRequest quando Senha Invalida", typeof(string))]
        [SwaggerResponse(404, "NotFound Quando email não encontrado", typeof(string))]
        [SwaggerResponse(500, "Internal Server Error, When processing request", typeof(string))]
        [HttpPost("login")]
        
        public async Task<ActionResult<UsuarioResponse>> Login([FromBody] UsuarioRequest usuarioRequest)
        {
            //Consultar Usuario no banco atraves do Email
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email.Equals(usuarioRequest.Email));
            //Verificar se encontrou o usuário
            if (usuario == null)
            {
                return NotFound("Usuário Invalido!");
            }

            //Verificar se a Senha está correta
            if (usuario.Senha.Equals(usuarioRequest.Senha))
            {
                //Gerar o token
                var tokenHandler = new JwtSecurityTokenHandler();
                var chaveSecreta = Encoding.UTF8.GetBytes("3e8acfc238f45a314fd4b2bde272678ad30bd1774743a11dbc5c53ac71ca494b");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, usuario.Name),
                new Claim("Minha Claim","Oi"),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),

                }),
                    Expires = DateTime.UtcNow.AddHours(1), // Tempo de expiração do token

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveSecreta), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return new UsuarioResponse
                {
                    Id = usuario.Id,
                    Nome = usuario.Name,
                    Token = tokenString
                };
            }
            else
            {
                return BadRequest("Usuário/Senha Invalidos!");
            }

        }

        // GET: api/Usuarios
        [SwaggerResponse(401,"Não autorizado se credenciais são invalidas")]
        [SwaggerOperation(Summary = "Retorna lista de usuários",Description = "Retorna uma lista de usuários, contendo Id,Email,Nome")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            return await _context.Usuarios.Select(x => new UsuarioDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            }).ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return new UsuarioDto
            {

                Id = usuario.Id,
                Name = usuario.Name,
                Email = usuario.Email

            };
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> PutUsuario(int id, UsuarioUpdateDto usuarioDto)
        {
            if (id != usuarioDto.Id)
            {
                return BadRequest();
            }

            // Consultar e Obter usuario do banco

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Atribuir as propriedades de usuario no banco

            usuario.Name = usuarioDto.Name;
            usuario.Email = usuarioDto.Email;
            usuario.Senha = usuarioDto.Senha;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UsuarioCreateDto>> PostUsuario(UsuarioCreateDto usuarioDto)
        {
            var usuario = new Usuario
            {
                Email = usuarioDto.Email,
                Name = usuarioDto.Name,
                Senha = usuarioDto.Senha,
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
