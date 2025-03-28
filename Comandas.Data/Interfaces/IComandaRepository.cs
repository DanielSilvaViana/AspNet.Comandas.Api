using Comandas.Domain.Models;
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
        Task Add(Comanda novaComanda);
        Task<Comanda?> GetByIdAsync(int id);
        Task<ComandaGetDto> GetComandaAsync(int id);
        public Task<IEnumerable<ComandaGetDto>> GetComandas();
        Task SaveChangesAsync();
    }
}
