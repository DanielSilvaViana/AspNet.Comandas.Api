using Comandas.Services.Interfaces;
using Comandas.Shared.Dtos;
using Microsoft.Extensions.Logging;

namespace Comandas.Services
{
    public class MesaService : IMesaServices
    {
        private readonly ILogger<MesaService> _logger;

        public MesaService(ILogger<MesaService> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<MesaDto>> GetMesa()
        {
            throw new NotImplementedException();
        }
    }
}
