using Comandas.Data.Interfaces;
using Comandas.Domain.Models;
using Comandas.Services.Interfaces;
using Comandas.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Comandas.Services
{
    public class PedidoCozinhasServices : IPedidoCozinhasServices
    {
        private readonly IPedidoCozinhaRepository _pedidoCozinhaRepository;

        public PedidoCozinhasServices(IPedidoCozinhaRepository pedidoCozinhaRepository)
        {
            _pedidoCozinhaRepository = pedidoCozinhaRepository;
        }

        public Task<PedidoCozinha> DeletePedidoCozinhaAsync(int v, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PedidoCozinha> DeletePedidoCozinhaAsync(int id)
        {
            var pedido =  await _pedidoCozinhaRepository.DeletePedidoCozinhaAsync(id);
            return pedido;
        }

        public async Task<IEnumerable<PedidoCozinhaGetDto>> GetPedidoCozinhaAsync(int? situacaoID)
        {
            return await _pedidoCozinhaRepository.GetPedidoCozinhaAsync(situacaoID);
        }

        public async Task<PedidoCozinha> GetPedidoCozinhaByIdAsync(int id)
        {
            var pedidoCozinha = await _pedidoCozinhaRepository.GetPedidoCozinhaByIdAsync(id);

            return pedidoCozinha;


        }

        public async Task PutPedidoCozinhaAsync(int situacaoId, int id)
        {
            await _pedidoCozinhaRepository.PutPedidoCozinhaAsync(situacaoId, id);
        }
    }
}