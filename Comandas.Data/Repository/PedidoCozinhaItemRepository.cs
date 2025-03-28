using Comandas.Api.Data;
using Comandas.Data.Interfaces;
using Comandas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repository
{
    public class PedidoCozinhaItemRepository : IPedidoCozinhaItemRepository
    {
        private readonly AppDbContext _context;

        public PedidoCozinhaItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(PedidoCozinhaItem novoPedidoCozinhaItem)
        {
            await _context.PedidoCozinhaItems.AddAsync(novoPedidoCozinhaItem);
        }
    }
}
