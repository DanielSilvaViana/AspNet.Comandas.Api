using Comandas.Api.Data;
using Comandas.Data.Interfaces;
using Comandas.Domain.Models;
using Comandas.Shared.Dtos;
using Comandas.Shared.Exceptions;
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
        private readonly IComandaRepository _comandaRepository;

        public PedidoCozinhaRepository(AppDbContext context,IComandaRepository comandaRepository)
        {
            _context = context;
            _comandaRepository = comandaRepository;
        }

        public IComandaRepository ComandaRepository { get; }

        public async Task AddAsync(PedidoCozinha novoPedidoCozinha)
        {
            await _context.PedidoCozinhas.AddAsync(novoPedidoCozinha);
        }
       
        public async Task<IEnumerable<PedidoCozinhaGetDto>> GetPedidoCozinhaAsync(int? situacaoID)
        {
            var query = _context.PedidoCozinhas
             .Include(c => c.Comanda)
             .Include(pci => pci.PedidoCozinhaItens)
                 .ThenInclude(ci => ci.ComandaItem)
                     .ThenInclude(cai => cai.CardapioItem).AsQueryable();
            if (situacaoID > 0)
            {
                query = query.Where(w => w.SituacaoId == situacaoID);
            }
            return await query
                .Select(s => new PedidoCozinhaGetDto
                {
                    Id = s.Id,
                    NumeroMesa = s.Comanda.NumeroMesa,
                    NomeCliente = s.Comanda.NomeCliente,
                    Titulo = s.PedidoCozinhaItens.First().ComandaItem.CardapioItem.Titulo
                }).ToListAsync();
        }

        public async Task<PedidoCozinha> GetPedidoCozinhaByIdAsync(int id)
        {
            var pedidoCozinha = await _context.PedidoCozinhas.FindAsync(id);

            if (pedidoCozinha == null)
            {
                throw new NotFoundException("Pedido Não localizado!");
            }

            return pedidoCozinha;
        }

        public async Task PutPedidoCozinhaAsync(int situacaoId, int id)
        {
            var pedido = await _context.PedidoCozinhas.FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                throw new NotFoundException("Pedido não localizado!");
            }

            pedido.SituacaoId = situacaoId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoCozinhaExists(id))
                {
                    throw new NotFoundException("Pedido não localizado!");
                }
                else
                {
                    throw;
                }
            }

        }

        public async Task<PedidoCozinha> DeletePedidoCozinhaAsync(int id)
        {
            var pedidoCozinha = await _context.PedidoCozinhas.FindAsync(id);
            if (pedidoCozinha == null)
            {
                throw new NotFoundException("Pedido não localizado!");
            }

            _context.PedidoCozinhas.Remove(pedidoCozinha);
            await _comandaRepository.SaveChangesAsync();
            return pedidoCozinha;
        }

        private bool PedidoCozinhaExists(int id)
        {
            return _context.PedidoCozinhas.Any(e => e.Id == id);
        }
    }
}

