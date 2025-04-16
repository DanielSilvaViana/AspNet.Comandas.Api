using Comandas.Api.Controllers;
using Comandas.Api.Data;
using Comandas.Api.Dtos;
using Comandas.Domain.Models;
using Comandas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Comandas.Testes
{
    public class PedidoCozinhaControllerTest
    {
        private readonly PedidoCozinhasController _controller;
        private readonly AppDbContext _appDbContext;
        private readonly IPedidoCozinhasServices _pedidoCozinhasServices;

        public PedidoCozinhaControllerTest()
        {
            var serviceProvider = new ServiceCollection().AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase(Guid.NewGuid().ToString())).BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            _appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            _pedidoCozinhasServices = scope.ServiceProvider.GetRequiredService<IPedidoCozinhasServices>();

            _controller = new PedidoCozinhasController(_appDbContext, _pedidoCozinhasServices);

            inserirDados();
        }

        private void inserirDados()
        {
            var mesa = new Mesa
            {
                NumeroMesa = 1,
                SituacaoMesa = 0,
            };
            var cardapioItem = new CardapioItem
            {
                Titulo = "Pizza",
                Descricao = "Pizza de Mussarela",
                Preco = 30.00m,
                PossuiPreparo = true
            };

            var comanda = new Comanda
            {
                NumeroMesa = 1,
                NomeCliente = "Cliente 1",
                SituacaoComanda = 1
            };

            var comandaItem = new ComandaItem
            {
                CardapioItem = cardapioItem,
                Comanda = comanda
            };

            var pedidoCozinha = new PedidoCozinha
            {
                Comanda = comanda,
                SituacaoId = 1
            };

            var pedidoCozinhaItem = new PedidoCozinhaItem
            {
                PedidoCozinha = pedidoCozinha,
                ComandaItem = comandaItem
            };

            _appDbContext.CardapioItems.Add(cardapioItem);
            _appDbContext.Comandas.Add(comanda);
            _appDbContext.ComandaItems.Add(comandaItem);
            _appDbContext.PedidoCozinhas.Add(pedidoCozinha);
            _appDbContext.PedidoCozinhaItems.Add(pedidoCozinhaItem);
            _appDbContext.Mesas.Add(mesa);

            _appDbContext.SaveChanges();
        }

        [Fact]
        public async void GetPedidoCozinhaList_Return_Pedidos()
        {
            //Arange
            int? situacao = 1;

            //Act
            var resultado = await _controller.GetPedidoCozinha(situacao);

            //Assert

            var okResultado = Assert.IsType<OkObjectResult>(resultado.Result);
            var pedidos = Assert.IsType<List<PedidoCozinhaGetDto>>(okResultado.Value);

            Assert.NotEmpty(pedidos);

        }
        [Fact]
        public async Task PutPedido_Update_Pedido()
        {
            //Arange

            var id = 1;
            var situacaoId = 1;

            //Act

            var resultado = await _controller.PutPedidoCozinha(id, situacaoId);

            //Assert
            var nonContentResultado = Assert.IsType<NoContentResult>(resultado);

            Assert.Equal(situacaoId, _appDbContext.PedidoCozinhas.First().SituacaoId);
        }
       
    }
}