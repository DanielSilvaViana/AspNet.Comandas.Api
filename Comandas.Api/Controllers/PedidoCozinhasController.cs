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

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidoCozinhasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PedidoCozinhas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoCozinhaGetDto>>> GetPedidoCozinha([FromQuery] int? situacaoID)
        {
            var query = _context.PedidoCozinhas
                .Include(c => c.Comanda)
                .Include(pci => pci.PedidoCozinhaItens)
                    .ThenInclude(ci => ci.ComandaItem)
                        .ThenInclude(cai => cai.CardapioItem).AsQueryable();
            if (situacaoID > 0)
            {
                query = query.Where(w => w.SituacaoId == situacaoID);
            }
            return Ok( await query
                .Select(s => new PedidoCozinhaGetDto
                {
                    Id = s.Id,
                    NumeroMesa = s.Comanda.NumeroMesa,
                    NomeCliente = s.Comanda.NomeCliente,
                    Titulo = s.PedidoCozinhaItens.First().ComandaItem.CardapioItem.Titulo
                }).ToListAsync());
        }

        // GET: api/PedidoCozinhas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoCozinha>> GetPedidoCozinha(int id)
        {
            var pedidoCozinha = await _context.PedidoCozinhas.FindAsync(id);

            if (pedidoCozinha == null)
            {
                return NotFound();
            }

            return pedidoCozinha;
        }

        // PUT: api/PedidoCozinhas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedidoCozinha(int id, [FromQuery] int situacaoId)
        {
            var pedido = await _context.PedidoCozinhas.FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound("Pedido não encontrado!");
            }

            pedido.SituacaoId = situacaoId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoCozinhaExists(id))
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

        // DELETE: api/PedidoCozinhas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidoCozinha(int id)
        {
            var pedidoCozinha = await _context.PedidoCozinhas.FindAsync(id);
            if (pedidoCozinha == null)
            {
                return NotFound();
            }

            _context.PedidoCozinhas.Remove(pedidoCozinha);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoCozinhaExists(int id)
        {
            return _context.PedidoCozinhas.Any(e => e.Id == id);
        }
    }
}
