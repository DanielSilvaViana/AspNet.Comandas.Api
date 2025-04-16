using Comandas.Data.Interfaces;
using Comandas.Domain.Models;
using Comandas.Services.Interfaces;
using Comandas.Shared.Dtos;
using Comandas.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Services
{
    public class CardapioItemServices : ICardapioItemServices
    {

        private readonly ICardapioItemRepository _cardapioItemRepository;

        public CardapioItemServices(ICardapioItemRepository cardapioItemRepository)
        {
            _cardapioItemRepository = cardapioItemRepository;
        }



        public async Task<IEnumerable<CardapioItemDto>> GetCardapioItems()
        {
            return await _cardapioItemRepository.GetCardapioItemsAsync();


        }


        public async Task<CardapioItemDto> GetCardapioItemsAsync(int id)
        {
            var cardapioItem = await _cardapioItemRepository.GetCardapioItemsById(id);

            return cardapioItem;
        }

        public async Task<CardapioItem> PostCardapioItemAsync(CardapioCreateDto cardapioItemDto)
        {
            var cardapio = await _cardapioItemRepository.PostCardapioItemAsync(cardapioItemDto);

            return cardapio;

        }



        public async Task PutCardapioItemAsync(CardapioUpdateDto cardapioItemDto, int id)
        {
            await _cardapioItemRepository.PutCardapioItemAsync(cardapioItemDto, id);
        }

        public async Task DeleteCardapioAsync(int id)
        {
            await _cardapioItemRepository.DeleteCardapioAsync(id);

        }

    }
}
