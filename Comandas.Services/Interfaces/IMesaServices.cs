using Comandas.Shared.Dtos;
using System;

namespace Comandas.Services.Interfaces;

public interface IMesaServices
{
    Task<IEnumerable<MesaDto>> GetMesa();
}
