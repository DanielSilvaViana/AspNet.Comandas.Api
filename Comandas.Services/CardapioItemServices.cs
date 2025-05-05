using Comandas.Data.Interfaces;
using Comandas.Data.Repository;
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
        private readonly IRedisService _redisService;

        public CardapioItemServices(ICardapioItemRepository cardapioItemRepository, IRedisService redisService)
        {
            _cardapioItemRepository = cardapioItemRepository;
            _redisService = redisService;
        }



        public async Task<IEnumerable<CardapioItemDto>> GetCardapioItems()
        {
            return await _cardapioItemRepository.GetCardapioItemsAsync();
        }

        public async Task<CardapioItemDto> GetCardapioItemsAsync(int id)
        {
            var key = $"cardapioItem:{id}";
            var keyExists = await _redisService.KeyExistsAsync(key);
            if (keyExists)
            {
                return await _redisService.GetAsync<CardapioItemDto>(key);
            }

            var cardapioItem = await _cardapioItemRepository.GetCardapioItemsById(id);
            await _redisService.SetAsync(key, cardapioItem, TimeSpan.FromMinutes(60));

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
