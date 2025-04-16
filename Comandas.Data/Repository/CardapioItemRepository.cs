using Comandas.Api.Data;
using Comandas.Data.Interfaces;
using Comandas.Domain.Models;
using Comandas.Shared.Dtos;
using Comandas.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repository
{
    public class CardapioItemRepository : ICardapioItemRepository
    {
        private readonly AppDbContext _context;
        private readonly IComandaRepository _comandaRepository;


        public CardapioItemRepository(AppDbContext context, IComandaRepository comandaRepository)
        {
            _context = context;
            _comandaRepository = comandaRepository;
        }

        public async Task DeleteCardapioAsync(int id)
        {
            var cardapioItem = await _context.CardapioItems.FindAsync(id);
            if (cardapioItem == null)
            {
                throw new NotFoundException("Cardapio Não Localizado!");
            }

            _context.CardapioItems.Remove(cardapioItem);
            await _comandaRepository.SaveChangesAsync();

        }

        public async Task<CardapioItem> FindAsync(int item)
        {
            var cardapioItem = await _context.CardapioItems.FindAsync(item);

            return cardapioItem;
        }


        public async Task<IEnumerable<CardapioItemDto>> GetCardapioItemsAsync()
        {
            var retornoCardapio = await _context.CardapioItems.Select(x => new CardapioItemDto
            {
                Id = x.Id,
                Descricao = x.Descricao,
                PossuiPreparo = x.PossuiPreparo,
                Preco = x.Preco,
                Titulo = x.Titulo
            }).ToListAsync();
            return retornoCardapio;
        }



        public async Task<CardapioItemDto> GetCardapioItemsById(int id)
        {
            var cardapioItem = await _context.CardapioItems.AsNoTracking().TagWith(nameof(GetCardapioItemsAsync)).FirstOrDefaultAsync(x => x.Id == id);

            if (cardapioItem == null)
            {
                throw new NotFoundException("Cardapio Não Cadastradao!");
            }

            var retornoCardapio = new CardapioItemDto
            {
                Id = cardapioItem.Id,
                Descricao = cardapioItem.Descricao,
                PossuiPreparo = cardapioItem.PossuiPreparo,
                Titulo = cardapioItem.Titulo,
                Preco = cardapioItem.Preco
            };
            return retornoCardapio;
        }

        public async Task<CardapioItem> PostCardapioItemAsync(CardapioCreateDto cardapioItemDto)
        {
            var cardapio = new CardapioItem
            {
                Titulo = cardapioItemDto.Titulo,
                Descricao = cardapioItemDto.Descricao,
                PossuiPreparo = cardapioItemDto.PossuiPreparo,
                Preco = cardapioItemDto.Preco
            };

            _context.CardapioItems.Add(cardapio);
            await _comandaRepository.SaveChangesAsync();

            return cardapio;
        }

        public async Task PutCardapioItemAsync(CardapioUpdateDto cardapioItemDto, int id)
        {


            if (id != cardapioItemDto.Id)
            {
                throw new NotFoundException("ID não Localizado!");

            }

            //Consultar e Obter cardapio do banco

            var cardapio = await _context.CardapioItems.FindAsync(id);

            if (cardapio == null)
            {
                throw new NotFoundException("Cardapio não Localizado!");
            }

            //Atribuir as propriedades de usuário no banco

            cardapio.Titulo = cardapioItemDto.Titulo;
            cardapio.Preco = cardapioItemDto.Preco;
            cardapio.Descricao = cardapioItemDto.Descricao;
            cardapio.PossuiPreparo = cardapioItemDto.PossuiPreparo;

            try
            {
                await _comandaRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardapioItemExists(id))
                {
                    throw new NotFoundException("Cardapio não Localizado!");

                }
                else
                {
                    throw;
                }
            }

            
        }
        private bool CardapioItemExists(int id)
        {
            return _context.CardapioItems.Any(e => e.Id == id);
        }

    }
}
         


