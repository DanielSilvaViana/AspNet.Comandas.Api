using Comandas.Services.Interfaces;
using Comandas.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Services
{
    public class ComandaServices : IComandaServices
    {
        private readonly IComandaRepository _comandaRepository;
        public async Task<IEnumerable<ComandaGetDto>> GetComandas()
        {
           
        }
    }
}
