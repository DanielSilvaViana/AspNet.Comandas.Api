using Comandas.Api.Data;
using Comandas.Data.Interfaces;
using Comandas.Domain.Models;
using Comandas.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repository
{
    public class MesaRepository : IMesaRepository
    {
        public readonly AppDbContext _appDbContext;

        public MesaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Mesa> GetMesaAsync(int numeroMesa)
        {
            var mesa = await _appDbContext.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == numeroMesa);
            return mesa;
        }

        public Task<ComandaDto> PostComandaAsync()
        {
            throw new NotImplementedException();
        }
    }
}
