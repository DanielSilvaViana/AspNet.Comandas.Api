using Comandas.Domain.Models;
using Comandas.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Services.Interfaces
{
    public interface IPedidoCozinhasServices
    {
        Task<PedidoCozinha> DeletePedidoCozinhaAsync(int v, int id);
        Task<PedidoCozinha> DeletePedidoCozinhaAsync(int id);
        Task<IEnumerable<PedidoCozinhaGetDto>> GetPedidoCozinhaAsync(int? situacaoID);
        Task<PedidoCozinha> GetPedidoCozinhaByIdAsync(int id);
        Task PutPedidoCozinhaAsync(int situacaoId, int id);
    }
}
