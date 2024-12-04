 using Comandas.Api.Data;
using Comandas.Api.Dtos;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private const int SITUACAO_ABERTA = 1;
        private const int SITUACAO_MESA_OCUPADA = 1;
        private const int SITUACAO_MESA_DISPONIVEL = 0;
        private const int SITUACAO_PEDIDO_PENDENTE = 1;
        private const int SITUACAO_COMANDA_ENCERRADA = 2;



        public ComandaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ComandaGetDto>>> GetComandas()
        {
            var comandas = await _context.Comandas
                .Where(c => c.SituacaoComanda == SITUACAO_ABERTA)
                .Select(C => new ComandaGetDto
                {

                    Id = C.Id,
                    NumeroMesa = C.NumeroMesa,
                    NomeCliente = C.NomeCliente,
                    SituacaoComanda = C.SituacaoComanda,
                    ComandaItems = C.ComandaItems
                   .Select(ci => new ComandaItemsGetDto { Id = ci.Id, Titulo = ci.CardapioItem.Titulo })
                   .ToList(),

                }).ToListAsync();

            return Ok(comandas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComandaGetDto>> GetComanda(int id)
        {
            var comanda = await _context.Comandas.FirstOrDefaultAsync(x => x.Id == id);
            if (comanda == null)
            {
                return NotFound("Comanda Não Encontrada!");
            }
            var comandaDto = new ComandaGetDto
            {
                Id = comanda.Id,
                NumeroMesa = comanda.NumeroMesa,
                NomeCliente = comanda.NomeCliente
            };
            var comandaItemsDto = await _context.ComandaItems.
                Include(ci => ci.CardapioItem).
                Where(x => x.ComandaId == id).
                Select(s => new ComandaItemsGetDto
                {
                    Id = s.Id,
                    Titulo = s.CardapioItem.Titulo,
                }).ToListAsync();

            comandaDto.ComandaItems = comandaItemsDto;
            return Ok(comandaDto);
        }

        [HttpPost]
        public async Task<ActionResult<ComandaDto>> PostComanda(ComandaDto comandadto)
        {
            var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == comandadto.NumeroMesa);
            if (mesa is null)
            {
                return BadRequest("Mesa não encontrada!");
            }
            if (mesa.SituacaoMesa != 0)
            {
                return BadRequest("Mesa Ocupada!");
            }

            mesa.SituacaoMesa = SITUACAO_MESA_OCUPADA;

            var novaComanda = new Comanda
            {
                NumeroMesa = comandadto.NumeroMesa,
                NomeCliente = comandadto.NomeCliente
            };

            _context.Comandas.Add(novaComanda);

            foreach (var item in comandadto.CardapioItems)
            {
                var novoComandaItem = new ComandaItem
                {
                    Comanda = novaComanda,
                    CardapioItemId = item
                };

                await _context.ComandaItems.AddAsync(novoComandaItem);
                var cardapioItem = await _context.CardapioItems.FindAsync(item);

                if (cardapioItem is null)
                {
                    return BadRequest("Cardápio Inválido!");
                }
                if (cardapioItem.PossuiPreparo)
                {
                    var novoPedidoCozinha = new PedidoCozinha
                    {
                        Comanda = novaComanda
                    };

                    await _context.PedidoCozinhas.AddAsync(novoPedidoCozinha);

                    var novoPedidoCozinhaItem = new PedidoCozinhaItem
                    {
                        PedidoCozinha = novoPedidoCozinha,
                        ComandaItem = novoComandaItem
                    };

                    await _context.PedidoCozinhaItems.AddAsync(novoPedidoCozinhaItem);
                }
            }

            _context.SaveChanges();
            return CreatedAtAction(nameof(GetComanda), new { id = novaComanda.Id }, comandadto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComanda(int id, ComandaUpdateDto comandaUpdateDto)
        {
            if (id != comandaUpdateDto.Id) return BadRequest();

            var comanda = await _context.Comandas.FirstOrDefaultAsync(c => c.Id == comandaUpdateDto.Id);
            if (comandaUpdateDto.NumeroMesa > 0)
            {
                var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == comandaUpdateDto.NumeroMesa);
                if (mesa is null)
                {
                    return BadRequest("Mesa Não Encontrada!");
                }
                if (mesa.SituacaoMesa != 0)
                {
                    return BadRequest("Mesa Ocupada");
                }

                mesa.SituacaoMesa = SITUACAO_MESA_OCUPADA;

                var mesaAtual = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == comanda.NumeroMesa);
                mesaAtual.SituacaoMesa = SITUACAO_MESA_DISPONIVEL;

                comanda.NumeroMesa = comandaUpdateDto.NumeroMesa;
            }

            if (!string.IsNullOrEmpty(comandaUpdateDto.NomeCliente))
                comanda.NomeCliente = comandaUpdateDto.NomeCliente;

            foreach (var item in comandaUpdateDto.ComandaItens)
            {
                if (item.incluir)
                {
                    var novoComandaItem = new ComandaItem
                    {
                        Comanda = comanda,
                        CardapioItemId = item.cardapioItemId
                    };
                    await _context.ComandaItems.AddAsync(novoComandaItem);

                    var cardapioItem = await _context.CardapioItems.FirstOrDefaultAsync(ca => ca.Id == item.cardapioItemId);

                    if (cardapioItem is null)
                    {
                        return BadRequest("Cardapio não encontrado!");
                    }
                    if (cardapioItem.PossuiPreparo)
                    {
                        var pedidoCozinha = new PedidoCozinha
                        {
                            Comanda = comanda,
                            SituacaoId = SITUACAO_MESA_DISPONIVEL

                        };
                        await _context.PedidoCozinhas.AddAsync(pedidoCozinha);
                        var pedidoCozinhaItem = new PedidoCozinhaItem
                        {
                            PedidoCozinha = pedidoCozinha,
                            ComandaItem = novoComandaItem
                        };
                        await _context.PedidoCozinhaItems.AddAsync(pedidoCozinhaItem);
                    }
                }
                if (item.excluir)
                {
                    var comandaItemExcluir = await _context.ComandaItems.FirstOrDefaultAsync(ci => ci.Id == item.Id);

                    if (comandaItemExcluir is null)
                    {
                        return BadRequest("Item da comanda informado inválido!");

                    }
                    _context.ComandaItems.Remove(comandaItemExcluir);

                }

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Comandas.Any(c => c.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();

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
