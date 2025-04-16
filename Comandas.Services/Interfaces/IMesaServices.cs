using Comandas.Domain.Models;
using Comandas.Shared.Dtos;
using System;

namespace Comandas.Services.Interfaces;

public interface IMesaServices
{
    Task<Mesa> DeleteMesa(int id);
    Task<IEnumerable<MesaDto>> GetMesa();
    Task<MesaDto> GetMesaById(int id);
    Task<Mesa> PostMesaAsync(MesaCreateDto mesaDto);
    Task PutMesaAsync(MesaUpdateDto mesadto, int id);
}
