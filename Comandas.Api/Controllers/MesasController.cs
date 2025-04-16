using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comandas.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Comandas.Services;
using Comandas.Services.Interfaces;
using Comandas.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;
using Comandas.Shared.Exceptions;
using Comandas.Shared.Dtos;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Tags("2. Mesas")]

    public class MesasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMesaServices _mesaServices;

        public MesasController(AppDbContext context, IMesaServices mesasServices)
        {
            _context = context;
            _mesaServices = mesasServices;
        }

        // GET: api/Mesas
        /// <summary>
        /// Retorna lista Mesa
        /// </summary>
        /// <returns>Retorna uma Lista de IEnumerable<MesaDto></returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Retorna uma lista e situação de Mesas", Description = "recupera uma lista de mesas")]
        [SwaggerResponse(200, "retorna uma lista e situação de Mesa", typeof(List<MesaDto>))]
        [SwaggerResponse(401, "Acesso não autorizado,se credenciais inválidas")]
        [SwaggerResponse(500, "Erro interno do servidor, ao processar a requisição")]
        public async Task<ActionResult<IEnumerable<MesaDto>>> GetMesa()
        {
            var mesa = await _mesaServices.GetMesa();          

            return Ok(mesa);
        }

        // GET: api/Mesas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MesaDto>> GetMesa(int id)
        {
            try
            {
                var mesa = await _mesaServices.GetMesaById(id);

                return Ok(mesa);
            }
            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);
            }
         
        }

        // PUT: api/Mesas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMesa(int id, MesaUpdateDto mesadto)
        {
            try
            {
                await _mesaServices.PutMesaAsync(mesadto, id);
                return NoContent();
            }
            catch (BadRequestException ex)
            {

                return BadRequest(ex.Message);
            }
           

        }

        // POST: api/Mesas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MesaCreateDto>> PostMesa(MesaCreateDto mesaDto)
        {
            try
            {
                var mesa = await _mesaServices.PostMesaAsync(mesaDto);

                return CreatedAtAction("GetMesa", new { id = mesa.Id }, mesa);
            }
            catch (BadRequestException ex)
            {

                return BadRequest(ex.Message);
            }

           
        }

        // DELETE: api/Mesas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMesa(int id)
        {
            var mesa = await _mesaServices.DeleteMesa(id);

          

            return NoContent();
        }

        private bool MesaExists(int id)
        {
            return _context.Mesas.Any(e => e.Id == id);
        }
    }
}
