using Comandas.Api.Controllers;
using Comandas.Api.Data;
using Comandas.Api.Dtos;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Comandas.Testes
{
    public class MesaControllerTest
    {
        private readonly MesasController _controller;
        private readonly AppDbContext _appDbContext;

        //public MesaControllerTest()
        //{
        //    var serviceProvider = new ServiceCollection().AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase(Guid.NewGuid().ToString())).BuildServiceProvider();
        //    var scope = serviceProvider.CreateScope();
        //    _appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        //    _controller = new MesasController(_appDbContext);

        //    inserirDados();
        //}

        private void inserirDados()
        {
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

            var mesa = new Mesa
            {
                NumeroMesa = 1,
                SituacaoMesa = 0,

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
        public async void GetMesa_Return_Open_Mesas()
        {
            //Arange

            //Act

            var result = await _controller.GetMesa();

            //Assert
            var oK = Assert.IsType<OkObjectResult>(result.Result);
            var mesas = Assert.IsType<List<MesaDto>>(oK.Value);
            Assert.NotEmpty(mesas);

        }

        [Fact]
        public async Task GetMesa_Return_Mesa_ById()
        {
            //Arange

            var mesasId = _appDbContext.Mesas.First().Id;
            //Act
            var result = await _controller.GetMesa(mesasId);

            //Assert
            var Ok = Assert.IsType<OkObjectResult>(result.Result);
            var mesaById = Assert.IsType<MesaDto>(Ok.Value);
            Assert.Equal(mesaById.Id, mesasId);
        }

      
    }
}