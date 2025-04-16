using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comandas.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Comandas.Domain.Models;
using Comandas.Services.Interfaces;
using Comandas.Data.Interfaces;
using Comandas.Data.Repository;
using Comandas.Shared.Exceptions;
using Comandas.Shared.Dtos;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [Tags("1. Cardapios")]

    public class CardapioItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICardapioItemServices _cardapioItemServices;


        public CardapioItemsController(AppDbContext context, ICardapioItemServices cardapioItemRepository)
        {
            _context = context;
            _cardapioItemServices = cardapioItemRepository;
        }

        // GET: api/CardapioItems
        /// <summary>
        /// Retorna uma lista de cardapio
        /// </summary>
        /// <returns>Retorna um IENumerable<CardapioItemDto></returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Retorna uma lista de cardapio", Description = "recupera uma lista de cardapio itens")]
        [SwaggerResponse(200, "retorna uma lista de cardapio", typeof(List<CardapioItemDto>))]
        [SwaggerResponse(401, "Acesso não autorizado,se credenciais inválidas")]
        [SwaggerResponse(500, "Erro interno do servidor, ao processar a requisição")]
        public async Task<ActionResult<IEnumerable<CardapioItemDto>>> GetCardapioItems()
        {

            var retornoCardapio = await _cardapioItemServices.GetCardapioItems();

            return Ok(retornoCardapio);

        }

        // GET: api/CardapioItems/5
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna uma lista de cardapio por Id", Description = "recupera uma lista de cardapio itens por Id")]
        [SwaggerResponse(200, "retorna uma lista de cardapio", typeof(List<CardapioItemDto>))]
        [SwaggerResponse(401, "Acesso não autorizado,se credenciais inválidas")]
        [SwaggerResponse(500, "Erro interno do servidor, ao processar a requisição")]
        public async Task<ActionResult<CardapioItemDto>> GetCardapioItem(int id)
        {
            var cardapioItem = await _cardapioItemServices.GetCardapioItemsAsync(id);

            return Ok(cardapioItem);
        }

        // PUT: api/CardapioItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Altera e atualiza uma lista de cardapio por Id", Description = "Atualiza uma lista de cardapio itens por Id")]
        [SwaggerResponse(200, "retorna uma lista de cardapio", typeof(List<CardapioItemDto>))]
        [SwaggerResponse(401, "Acesso não autorizado,se credenciais inválidas")]
        [SwaggerResponse(500, "Erro interno do servidor, ao processar a requisição")]
        public async Task<IActionResult> PutCardapioItem(int id, CardapioUpdateDto cardapioItemDto)
        {
            try
            {
                await _cardapioItemServices.PutCardapioItemAsync(cardapioItemDto, id);

                return NoContent();
            }
            catch (BadRequestException ex)
            {

                return BadRequest(ex.Message);
            }

        }

        // POST: api/CardapioItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [SwaggerOperation(Summary = "Faz uma chamada Post para localizar lista de cardapio", Description = "Post para retorno de uma lista de cardapio itens")]
        [SwaggerResponse(200, "retorna uma lista de cardapio", typeof(List<CardapioItemDto>))]
        [SwaggerResponse(401, "Acesso não autorizado,se credenciais inválidas")]
        [SwaggerResponse(500, "Erro interno do servidor, ao processar a requisição")]
        public async Task<ActionResult<CardapioItem>> PostCardapioItem(CardapioCreateDto cardapioItemDto)
        {
            try
            {
                var cardapio = await _cardapioItemServices.PostCardapioItemAsync(cardapioItemDto);

                return CreatedAtAction("GetCardapioItem", new { id = cardapioItemDto.Id }, cardapio);
            }
            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Erro interno do Servidor");
            }

        }

        // DELETE: api/CardapioItems/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "EndPoint para deletar lista de cardapios", Description = "Deletar lista por Id")]
        [SwaggerResponse(200, "retorna uma lista de cardapio", typeof(List<CardapioItemDto>))]
        [SwaggerResponse(401, "Acesso não autorizado,se credenciais inválidas")]
        [SwaggerResponse(500, "Erro interno do servidor, ao processar a requisição")]
        public async Task<IActionResult> DeleteCardapioItem(int id)
        {
            try
            {
                await _cardapioItemServices.DeleteCardapioAsync(id);

            }
            catch (NotFoundException ex)
            {

                return NotFound(ex.Message);

            }
            return NoContent();

        }

        private bool CardapioItemExists(int id)
        {
            return _context.CardapioItems.Any(e => e.Id == id);
        }
    }
}
