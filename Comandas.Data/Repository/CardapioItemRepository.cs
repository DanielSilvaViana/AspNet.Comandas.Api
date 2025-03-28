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
    public class CardapioItemRepository : ICardapioItemRepository
    {
        private readonly AppDbContext _context;

        public CardapioItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CardapioItem> FindAsync(int item)
        {
            var cardapioItem = await _context.CardapioItems.FindAsync(item);

            return cardapioItem;
        }
    }
}
