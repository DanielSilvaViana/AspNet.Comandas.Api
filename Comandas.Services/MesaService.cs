using Comandas.Data.Interfaces;
using Comandas.Domain.Models;
using Comandas.Services.Interfaces;
using Comandas.Shared.Dtos;
using Comandas.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Comandas.Services
{
    public class MesaService : IMesaServices
    {
        private readonly ILogger<MesaService> _logger;
        private readonly IMesaRepository _mesaRepository;
        private readonly IComandaServices _comandaServices;
        private readonly IRedisService _redisService;


        public MesaService(ILogger<MesaService> logger, IMesaRepository mesaRepository, IComandaServices comandaServices, IRedisService redisService)
        {
            _logger = logger;
            _mesaRepository = mesaRepository;
            _comandaServices = comandaServices;
            _redisService = redisService;
        }

        public async Task<Mesa> DeleteMesa(int id)
        {
            var key = $"mesaAll";
            var keyExists = await _redisService.KeyExistsAsync(key);
            if (keyExists)
            {
                await _redisService.RemoveAsync(key);
            }

            var mesa = await _mesaRepository.DeleteMesaAsync(id);
            return mesa;
        }

        public async Task<IEnumerable<MesaDto>> GetMesa()
        {
            var key = $"mesaAll";
            var keyExists = await _redisService.KeyExistsAsync(key);
            if (keyExists)
            {
                return await _redisService.GetAsync<List<MesaDto>>(key);
            }
            var mesas = await _mesaRepository.GetMesa();
            await _redisService.SetAsync(key, mesas, TimeSpan.FromMinutes(60));

            return mesas;

        }

        public async Task<MesaDto> GetMesaById(int id)
        {
            var key = $"mesa:{id}";
            var keyExists = await _redisService.KeyExistsAsync(key);
            if (keyExists)
            {
                return await _redisService.GetAsync<MesaDto>(key);
            }

            var mesa = await _mesaRepository.GetMesaByIdAsync(id);
            await _redisService.SetAsync(key, mesa, TimeSpan.FromMinutes(60));
            return mesa;

        }

        public async Task<Mesa> PostMesaAsync(MesaCreateDto mesaDto)
        {
            var key = $"mesaAll";
            var keyExists = await _redisService.KeyExistsAsync(key);
            if (keyExists)
            {
                 await _redisService.RemoveAsync(key);
            }

            var mesa = await _mesaRepository.PostMesaAsync(mesaDto);
            return mesa;


        }

        public async Task PutMesaAsync(MesaUpdateDto mesadto, int id)
        {
            var key = $"mesaAll";
            var keyExists = await _redisService.KeyExistsAsync(key);
            if (keyExists)
            {
                await _redisService.RemoveAsync(key);
            }

            await _mesaRepository.PutMesaAsync(mesadto, id);


        }
       
    }
}
