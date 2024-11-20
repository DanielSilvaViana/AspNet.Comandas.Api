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
    public class CardapioItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CardapioItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CardapioItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardapioItemDto>>> GetCardapioItems()
        {
            return await _context.CardapioItems.Select(x => new CardapioItemDto
            {
                Id = x.Id,
                Descricao = x.Descricao,
                PossuiPreparo = x.PossuiPreparo,
                Preco = x.Preco,
                Titulo = x.Titulo          
            }).ToListAsync();
        }

        // GET: api/CardapioItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardapioItemDto>> GetCardapioItem(int id)
        {
            var cardapioItem = await _context.CardapioItems.FindAsync(id);

            if (cardapioItem == null)
            {
                return NotFound();
            }

            return new CardapioItemDto
            {
                Id = cardapioItem.Id,
                Descricao = cardapioItem.Descricao,
                PossuiPreparo = cardapioItem.PossuiPreparo,
                Titulo= cardapioItem.Titulo,
                Preco = cardapioItem.Preco
            };           
        }

        // PUT: api/CardapioItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardapioItem(int id, CardapioUpdateDto cardapioItemDto)
        {
            if (id != cardapioItemDto.Id)
            {
                return BadRequest();
            }

            //Consultar e Obter cardapio do banco

            var cardapio = await _context.CardapioItems.FindAsync(id);

            if (cardapio == null)
            {
                return NotFound();
            }

            //Atribuir as propriedades de usuário no banco

            cardapio.Titulo = cardapioItemDto.Titulo;
            cardapio.Preco = cardapioItemDto.Preco;
            cardapio.Descricao = cardapioItemDto.Descricao;
            cardapio.PossuiPreparo = cardapioItemDto.PossuiPreparo;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardapioItemExists(id))
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

        // POST: api/CardapioItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardapioItem>> PostCardapioItem(CardapioCreateDto cardapioItemDto)
        {
            var cardapio = new CardapioItem
            {
                Titulo = cardapioItemDto.Titulo,
                Descricao = cardapioItemDto.Descricao,
                PossuiPreparo = cardapioItemDto.PossuiPreparo,
                Preco = cardapioItemDto.Preco
            };

            _context.CardapioItems.Add(cardapio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCardapioItem", new { id = cardapio.Id }, cardapio);
        }

        // DELETE: api/CardapioItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardapioItem(int id)
        {
            var cardapioItem = await _context.CardapioItems.FindAsync(id);
            if (cardapioItem == null)
            {
                return NotFound();
            }

            _context.CardapioItems.Remove(cardapioItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardapioItemExists(int id)
        {
            return _context.CardapioItems.Any(e => e.Id == id);
        }
    }
}
