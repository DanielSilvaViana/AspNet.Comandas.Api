
using Comandas.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Services.Interfaces
{
    public interface IComandaServices
    {
        Task<ComandaGetDto> GetComandaAsync(int id);
        Task<IEnumerable<ComandaGetDto>> GetComandas();
        Task<ComandaCreateDto> PostComandaAsync(ComandaDto comandadto);
        Task PutComandaAsync(ComandaUpdateDto comandaUpdateDto);
    }
}
