using Comandas.Api.Data;
using Comandas.Data.Interfaces;
using Comandas.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Data
{
    public class ComandaRepository: IComandaRepository
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
    }
}
