using Comandas.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Interfaces
{
    public interface IComandaRepository
    {
       public Task<IEnumerable<ComandaGetDto>> GetComandas();

    }
}
