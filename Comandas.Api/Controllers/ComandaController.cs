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
        private readonly IComandaServices _comandaServices;
        private const int SITUACAO_ABERTA = 1;
        private const int SITUACAO_MESA_OCUPADA = 1;
        private const int SITUACAO_MESA_DISPONIVEL = 0;
        private const int SITUACAO_PEDIDO_PENDENTE = 1;
        private const int SITUACAO_COMANDA_ENCERRADA = 2;


        private readonly ILogger<ComandaController> _logger;
        public ComandaController(
            IComandaServices comandaServices, ILogger<ComandaController> logger)
        {
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
            catch (Exception ex)
            {
                _logger.LogError("Erro Interno não tratado", ex);
                return StatusCode(500, "Erro Interno no Servidor!");
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

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Erro Interno do Servidor!");
            }



        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComanda(int id)
        {
            try
            {
                await _comandaServices.DeleteComandaAsync(id);
            }
            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);
            }


            return NoContent();
        }

        [HttpPatch("{id}")]

        public async Task<ActionResult> PatchComanda(int id)
        {
            try
            {
                await _comandaServices.PatchComandaAsync(id);
            }
            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);
            }

            return NoContent();

        }
    }

}
