using Comandas.Api.Data;
using Comandas.Domain.Models;
using Comandas.Services.Interfaces;
using Comandas.Shared.Dtos;
using Comandas.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Tags("3. Comanda")]

    public class ComandaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IComandaServices _comandaServices;
        private const int SITUACAO_ABERTA = 1;
        private const int SITUACAO_MESA_OCUPADA = 1;
        private const int SITUACAO_MESA_DISPONIVEL = 0;
        private const int SITUACAO_PEDIDO_PENDENTE = 1;
        private const int SITUACAO_COMANDA_ENCERRADA = 2;


        private readonly ILogger<ComandaController> _logger;
        public ComandaController(AppDbContext context, IComandaServices comandaServices, ILogger<ComandaController> logger)
        {
            _context = context;
            _comandaServices = comandaServices;
            _logger = logger;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ComandaGetDto>>> GetComandas()
        {
            var comandas = await _comandaServices.GetComandas();

            return Ok(comandas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComandaGetDto>> GetComanda(int id)
        {
            try
            {
                var comanda = await _comandaServices.GetComandaAsync(id);
                return Ok(comanda);
            }
            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);
            }
            catch(Exception ex) 
            {
                _logger.LogError("Erro Interno não tratado", ex);
                return StatusCode(500,"Erro Interno no Servidor!");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<ComandaDto>> PostComanda(ComandaDto comandadto)
        {
            try
            {
                var comanda = await _comandaServices.PostComandaAsync(comandadto);
                return CreatedAtAction(nameof(GetComanda), new { id = comanda.Id }, comanda);

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro Interno não Tratado", ex);
                return StatusCode(500, "Erro Interno no Servidor!");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComanda(int id, ComandaUpdateDto comandaUpdateDto)
        {
            try
            {
                await _comandaServices.PutComandaAsync(comandaUpdateDto);
                return NoContent();

            }
            catch (BadRequestException ex)
            {

                return BadRequest(ex.Message);
            }

            catch(Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                return StatusCode(500,"Erro Interno do Servidor!");
            }

            

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteComanda(string id)
        {
            var comanda = _context.Comandas.Find(id);
            if (comanda == null) return NotFound();

            _context.Comandas.Remove(comanda);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]

        public async Task<ActionResult> PatchComanda(int id)
        {
            //Consultar a Comanda
            var consultaComanda = await _context.Comandas.FirstOrDefaultAsync(comanda => comanda.Id == id);

            if (consultaComanda == null)
            {
                return NotFound("Comanda Não Encontrada!");
            }

            //Alterar a Situação da Comanda

            consultaComanda.SituacaoComanda = SITUACAO_COMANDA_ENCERRADA;

            //Liberar a Mesa
            var mesa = await _context.Mesas.FirstOrDefaultAsync(mesa => mesa.NumeroMesa == consultaComanda.NumeroMesa);

            if (mesa == null)
            {
                return NotFound("Mesa Não Encontrada!");
            }

            mesa.SituacaoMesa = SITUACAO_MESA_DISPONIVEL;

            // Salvar as Alterações no banco

            await _context.SaveChangesAsync();

            //Retornar um NonContent
            return NoContent();
        }
    }

}
