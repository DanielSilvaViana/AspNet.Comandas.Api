using Comandas.Domain.Models;
using Comandas.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Interfaces
{
    public interface IMesaRepository
    {
        Task<Mesa> GetMesaAsync(int numeroMesa);
        Task<ComandaDto> PostComandaAsync();
    }
}
