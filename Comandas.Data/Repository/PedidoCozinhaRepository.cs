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
    public class PedidoCozinhaRepository : IPedidoCozinhaRepository
    {
        private readonly AppDbContext _context;

        public PedidoCozinhaRepository(AppDbContext context)
        {
            _context = context;
        }       
        public async Task AddAsync(PedidoCozinha novoPedidoCozinha)
        {
            await _context.PedidoCozinhas.AddAsync(novoPedidoCozinha);
        }
    }
}
