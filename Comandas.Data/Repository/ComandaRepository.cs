using Comandas.Api.Data;
using Comandas.Data.Interfaces;
using Comandas.Domain.Models;
using Comandas.Shared.Dtos;
using Comandas.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Data.Repository
{
    public class ComandaRepository : IComandaRepository
    {
        private readonly AppDbContext _context;
        private const int SITUACAO_ABERTA = 1;


        public ComandaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComandaGetDto>> GetComandas()
        {
            var comandas = await _context.Comandas
              .Where(c => c.SituacaoComanda == SITUACAO_ABERTA)
              .Select(C => new ComandaGetDto
              {

                  Id = C.Id,
                  NumeroMesa = C.NumeroMesa,
                  NomeCliente = C.NomeCliente,
                  SituacaoComanda = C.SituacaoComanda,
                  ComandaItems = C.ComandaItems
                 .Select(ci => new ComandaItemsGetDto { Id = ci.Id, Titulo = ci.CardapioItem.Titulo })
                 .ToList(),

              }).ToListAsync();

            return comandas;
        }

        public async Task<ComandaGetDto> GetComandaAsync(int id)
        {
            var comanda = await _context.Comandas.FirstOrDefaultAsync(x => x.Id == id);
            if (comanda == null)
            {
                throw new NotFoundException("Comanda Não Encontrada!");
            }
            var comandaDto = new ComandaGetDto
            {
                Id = comanda.Id,
                NumeroMesa = comanda.NumeroMesa,
                NomeCliente = comanda.NomeCliente
            };
            var comandaItemsDto = await _context.ComandaItems.
                Include(ci => ci.CardapioItem).
                Where(x => x.ComandaId == id).
                Select(s => new ComandaItemsGetDto
                {
                    Id = s.Id,
                    Titulo = s.CardapioItem.Titulo,
                }).ToListAsync();

            comandaDto.ComandaItems = comandaItemsDto;
            return comandaDto;
        }

        public async Task Add(Comanda novaComanda)
        {
            await _context.Comandas.AddAsync(novaComanda);
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

        public async Task<Comanda> GetByIdAsync(int id)
        {
            return await _context.Comandas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public  void RemoverComanda(Comanda comanda)
        {
             _context.Comandas.Remove(comanda);
        }
    }
}
