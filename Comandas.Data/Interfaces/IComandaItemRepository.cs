using Comandas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Interfaces
{
    public interface IComandaItemRepository
    {
        Task AddAsync(ComandaItem novoComandaItem);
        Task<ComandaItem?> GetComandaItem(int id);
        void RemoveAsync(ComandaItem comandaItemExcluir);
    }
}
