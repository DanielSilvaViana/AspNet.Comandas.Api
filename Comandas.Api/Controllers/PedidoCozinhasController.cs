using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comandas.Api.Data;
using Comandas.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Comandas.Domain.Models;
using Comandas.Services.Interfaces;
using Comandas.Shared.Exceptions;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Tags("4. PedidosCozinhas")]

    public class PedidoCozinhasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPedidoCozinhasServices _pedidoCozinhas;

        public PedidoCozinhasController(AppDbContext context, IPedidoCozinhasServices pedidoCozinhas)
        {
            _context = context;
            _pedidoCozinhas = pedidoCozinhas;
        }

        // GET: api/PedidoCozinhas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoCozinhaGetDto>>> GetPedidoCozinha([FromQuery] int? situacaoID)
        {
            var query = await _pedidoCozinhas.GetPedidoCozinhaAsync(situacaoID);

            return Ok(query);
        }

        // GET: api/PedidoCozinhas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoCozinha>> GetPedidoCozinha(int id)
        {
            try
            {
                var pedidoCozinha = await _pedidoCozinhas.GetPedidoCozinhaByIdAsync(id);
                return pedidoCozinha;
            }
            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);
            }


        }

        // PUT: api/PedidoCozinhas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedidoCozinha(int id, [FromQuery] int situacaoId)
        {
            try
            {
                await _pedidoCozinhas.PutPedidoCozinhaAsync(situacaoId, id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);
            }           
        }

        // DELETE: api/PedidoCozinhas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidoCozinha(int id)
        {
            var pedidoCozinha = await _pedidoCozinhas.DeletePedidoCozinhaAsync(id);
            return NoContent();
        }       
    }
}
