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
        Task<IEnumerable<MesaDto>> GetMesa();
        Task<ComandaDto> PostComandaAsync();
        Task<MesaDto> GetMesaByIdAsync(int id);
        Task PutMesaAsync(MesaUpdateDto mesadto, int id);
        Task<Mesa> PostMesaAsync(MesaCreateDto mesaDto);
        Task<Mesa> DeleteMesaAsync(int id);
    }
}
