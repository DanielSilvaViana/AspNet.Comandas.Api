
using Comandas.Domain.Models;
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
        Task DeleteComandaAsync(int id);
        Task<ComandaGetDto> GetComandaAsync(int id);
        Task<IEnumerable<ComandaGetDto>> GetComandas();
        Task PatchComandaAsync(int id);
        Task<ComandaCreateDto> PostComandaAsync(ComandaDto comandadto);
        Task PutComandaAsync(ComandaUpdateDto comandaUpdateDto);
    }
}
