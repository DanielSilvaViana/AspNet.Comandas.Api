using Comandas.Data.Interfaces;
using Comandas.Domain.Models;
using Comandas.Services.Interfaces;
using Comandas.Shared.Dtos;
using Comandas.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Services
{
    public class ComandaServices : IComandaServices
    {
        private readonly IComandaRepository _comandaRepository;
        private readonly IMesaRepository _mesaRepository;
        private readonly IComandaItemRepository _comandaItemRepository;
        private readonly ICardapioItemRepository _cardapioItemRepository;
        private readonly IPedidoCozinhaRepository _pedidoRepository;
        private readonly IPedidoCozinhaItemRepository _pedidoCozinhaItemRepository;

        private const int SITUACAO_ABERTA = 1;
        private const int SITUACAO_MESA_OCUPADA = 1;
        private const int SITUACAO_MESA_DISPONIVEL = 0;
        private const int SITUACAO_PEDIDO_PENDENTE = 1;
        private const int SITUACAO_COMANDA_ENCERRADA = 2;


        public ComandaServices(IComandaRepository comandaRepository, IComandaItemRepository comandaItemRepository, IMesaRepository mesaRepository, ICardapioItemRepository cardapioItemRepository, IPedidoCozinhaRepository pedidoRepository, IPedidoCozinhaItemRepository pedidoCozinhaItemRepository)
        {
            _comandaRepository = comandaRepository;
            _comandaItemRepository = comandaItemRepository;
            _mesaRepository = mesaRepository;
            _cardapioItemRepository = cardapioItemRepository;
            _pedidoRepository = pedidoRepository;
            _pedidoCozinhaItemRepository = pedidoCozinhaItemRepository;
        }

        public async Task<ComandaGetDto> GetComandaAsync(int id)
        {
            var comanda = await _comandaRepository.GetComandaAsync(id);

            return comanda;
        }

        public async Task<IEnumerable<ComandaGetDto>> GetComandas()
        {
            return await _comandaRepository.GetComandas();
        }

        public async Task<ComandaCreateDto> PostComandaAsync(ComandaDto comandadto)
        {
            var mesa = await _mesaRepository.GetMesaAsync(comandadto.NumeroMesa);

            if (mesa is null)
            {
                throw new BadRequestException("Mesa não encontrada!");
            }
            if (mesa.SituacaoMesa != 0)
            {
                throw new BadRequestException("Mesa Ocupada!");
            }

            mesa.SituacaoMesa = SITUACAO_MESA_OCUPADA;

            var novaComanda = new Comanda
            {
                NumeroMesa = comandadto.NumeroMesa,
                NomeCliente = comandadto.NomeCliente
            };

            await _comandaRepository.Add(novaComanda);


            foreach (var item in comandadto.CardapioItems)
            {
                var novoComandaItem = new ComandaItem
                {
                    Comanda = novaComanda,
                    CardapioItemId = item
                };

                await _comandaItemRepository.AddAsync(novoComandaItem);
                var cardapioItem = await _cardapioItemRepository.FindAsync(item);


                if (cardapioItem is null)
                {
                    throw new BadRequestException("Cardápio Inválido!");
                }
                if (cardapioItem.PossuiPreparo)
                {
                    var novoPedidoCozinha = new PedidoCozinha
                    {
                        Comanda = novaComanda
                    };


                    await _pedidoRepository.AddAsync(novoPedidoCozinha);

                    var novoPedidoCozinhaItem = new PedidoCozinhaItem
                    {
                        PedidoCozinha = novoPedidoCozinha,
                        ComandaItem = novoComandaItem
                    };

                    await _pedidoCozinhaItemRepository.AddAsync(novoPedidoCozinhaItem);

                }
            }

            await _comandaRepository.SaveChangesAsync();
            //_context.SaveChanges();
            return new ComandaCreateDto { Id = novaComanda.Id, NomeCliente = novaComanda.NomeCliente, NumeroMesa = novaComanda.NumeroMesa, CardapioItems = comandadto.CardapioItems };
        }

        public async Task PutComandaAsync(ComandaUpdateDto comandaUpdateDto)
        {
            var comanda = await _comandaRepository.GetByIdAsync(comandaUpdateDto.Id);
            if (comandaUpdateDto.NumeroMesa > 0)
            {
                var mesa = await _mesaRepository.GetMesaAsync(comandaUpdateDto.NumeroMesa);

                if (mesa is null)
                {
                    throw new BadRequestException("Mesa Não Encontrada!");
                }
                if (mesa.SituacaoMesa != 0)
                {
                    throw new BadRequestException("Mesa Ocupada");
                }

                mesa.SituacaoMesa = SITUACAO_MESA_OCUPADA;

                var mesaAtual = await _mesaRepository.GetMesaAsync(comanda!.NumeroMesa);

                mesaAtual.SituacaoMesa = SITUACAO_MESA_DISPONIVEL;

                comanda.NumeroMesa = comandaUpdateDto.NumeroMesa;
            }

            if (!string.IsNullOrEmpty(comandaUpdateDto.NomeCliente))
                comanda.NomeCliente = comandaUpdateDto.NomeCliente;

            foreach (var item in comandaUpdateDto.ComandaItens)
            {
                if (item.incluir)
                {
                    var novoComandaItem = new ComandaItem
                    {
                        Comanda = comanda,
                        CardapioItemId = item.cardapioItemId
                    };

                    await _comandaItemRepository.AddAsync(novoComandaItem);

                    var cardapioItem = await _cardapioItemRepository.FindAsync(item.cardapioItemId);

                    if (cardapioItem is null)
                    {
                        throw new BadRequestException("Cardapio não encontrado!");
                    }
                    if (cardapioItem.PossuiPreparo)
                    {
                        var pedidoCozinha = new PedidoCozinha
                        {
                            Comanda = comanda,
                            SituacaoId = SITUACAO_MESA_DISPONIVEL

                        };

                        await _pedidoRepository.AddAsync(pedidoCozinha);

                        var pedidoCozinhaItem = new PedidoCozinhaItem
                        {
                            PedidoCozinha = pedidoCozinha,
                            ComandaItem = novoComandaItem
                        };

                        await _pedidoCozinhaItemRepository.AddAsync(pedidoCozinhaItem);

                    }
                }
                if (item.excluir)
                {
                    var comandaItemExcluir = await _comandaItemRepository.GetComandaItem(item.Id);

                    if (comandaItemExcluir is null)
                    {
                        throw new BadRequestException("Item da comanda informado inválido!");

                    }

                    _comandaItemRepository.RemoveAsync(comandaItemExcluir);
                }

            }

            await _comandaRepository.SaveChangesAsync();

        }
    }
}



