using Comandas.Api.Data;
using Comandas.Data.Interfaces;
using Comandas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repository
{
    public class ComandaItemRepository : IComandaItemRepository
    {
        private readonly AppDbContext _context;
        public ComandaItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ComandaItem novoComandaItem)
        {
            await _context.ComandaItems.AddAsync(novoComandaItem);
        }

        public async Task<ComandaItem?> GetComandaItem(int id)
        {
            return await _context.ComandaItems.FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public void RemoveAsync(ComandaItem comandaItemExcluir)
        {
            _context.ComandaItems.Remove(comandaItemExcluir);
        }
    }
}
