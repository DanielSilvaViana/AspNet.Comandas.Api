using Comandas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Interfaces
{
    public interface ICardapioItemRepository
    {
        Task<CardapioItem> FindAsync(int item);
    }
}
