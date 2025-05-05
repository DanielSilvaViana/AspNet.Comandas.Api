using Comandas.Data.Interfaces;
using Comandas.Data.Repository;
using Comandas.Domain.Models;
using Comandas.Services.Interfaces;
using Comandas.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Comandas.Services
{
    public class PedidoCozinhasServices : IPedidoCozinhasServices
    {
        private readonly IPedidoCozinhaRepository _pedidoCozinhaRepository;
        private readonly IRedisService _redisService;

        public PedidoCozinhasServices(IPedidoCozinhaRepository pedidoCozinhaRepository, IRedisService redisService)
        {
            _pedidoCozinhaRepository = pedidoCozinhaRepository;
            _redisService = redisService;
        }

        public Task<PedidoCozinha> DeletePedidoCozinhaAsync(int v, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PedidoCozinha> DeletePedidoCozinhaAsync(int id)
        {
            var pedido =  await _pedidoCozinhaRepository.DeletePedidoCozinhaAsync(id);
            return pedido;
        }

        public async Task<IEnumerable<PedidoCozinhaGetDto>> GetPedidoCozinhaAsync(int? situacaoID)
        {
            var key = $"pedidoCozinhaAll";
            var keyExists = await _redisService.KeyExistsAsync(key);
            if (keyExists)
            {
                return await _redisService.GetAsync<List<PedidoCozinhaGetDto>>(key);
            }
            var pedidoCozinhas = await _pedidoCozinhaRepository.GetPedidoCozinhaAsync(situacaoID);
            await _redisService.SetAsync(key, pedidoCozinhas, TimeSpan.FromMinutes(60));

            return pedidoCozinhas;
        }

        public async Task<PedidoCozinha> GetPedidoCozinhaByIdAsync(int id)
        {

            var key = $"pedidoCozinha:{id}";
            var keyExists = await _redisService.KeyExistsAsync(key);
            if (keyExists)
            {
                return await _redisService.GetAsync<PedidoCozinha>(key);
            }
           
            var pedidoCozinha = await _pedidoCozinhaRepository.GetPedidoCozinhaByIdAsync(id);
            await _redisService.SetAsync(key, pedidoCozinha, TimeSpan.FromMinutes(60));

            return pedidoCozinha;
        }

        public async Task PutPedidoCozinhaAsync(int situacaoId, int id)
        {
            await _pedidoCozinhaRepository.PutPedidoCozinhaAsync(situacaoId, id);
        }
    }
}