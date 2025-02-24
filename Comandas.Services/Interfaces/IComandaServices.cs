
using Comandas.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Services.Interfaces
{
    internal interface IComandaServices
    {
        Task<IEnumerable<ComandaGetDto>> GetComandas();
    }
}
