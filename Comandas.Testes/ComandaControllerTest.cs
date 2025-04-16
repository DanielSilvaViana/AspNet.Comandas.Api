using Castle.Core.Logging;
using Comandas.Api.Controllers;
using Comandas.Api.Data;
using Comandas.Api.Dtos;
using Comandas.Domain.Models;
using Comandas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Comandas.Testes
{
    public class ComandaControllerTest
    {
        private readonly ComandaController _controller;
        private readonly AppDbContext _appDbContext;
        private readonly IComandaServices _comandaServices;
        private readonly ILogger<ComandaController> _logger;


        public ComandaControllerTest()
        {
            var serviceProvider = new ServiceCollection().
                AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase(Guid.NewGuid().ToString())).AddLogging().BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            _appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            _comandaServices = scope.ServiceProvider.GetRequiredService<IComandaServices>();

            _logger = scope.ServiceProvider.GetRequiredService<ILogger<ComandaController>>();

            _controller = new ComandaController(_comandaServices,_logger);

            inserirDados();
        }

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

            _appDbContext.CardapioItems.Add(cardapioItem);
            _appDbContext.Comandas.Add(comanda);
            _appDbContext.ComandaItems.Add(comandaItem);
            _appDbContext.PedidoCozinhas.Add(pedidoCozinha);
            _appDbContext.PedidoCozinhaItems.Add(pedidoCozinhaItem);

            _appDbContext.SaveChanges();
        }

        [Fact]
        public async void GetComandas_Return_Open_Comandas()
        {
            //Arange

            //Act

            var result = await _controller.GetComandas();

            //Assert
            var oK = Assert.IsType<OkObjectResult>(result.Result);
            var comandas = Assert.IsType<List<ComandaGetDto>>(oK.Value);
            Assert.NotEmpty(comandas);

        }

        [Fact]
        public async Task GetComanda_Return_Comanda_ById()
        {
            //Arange

            var comandaId = _appDbContext.Comandas.First().Id;
            //Act
            var result = await _controller.GetComanda(comandaId);

            //Assert
            var Ok = Assert.IsType<OkObjectResult>(result.Result);
            var comandaById = Assert.IsType<ComandaGetDto>(Ok.Value);
            Assert.Equal(comandaById.Id,comandaId);
        }

        [Fact]

        public async Task GetComanda_Return_Not_Found_For_Invalid_Id()
        {
            //Arange

            var comandaIdInvalido = 99;
            //Act
            var result = await _controller.GetComanda(comandaIdInvalido);

            //Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);            

        }
      
    }
}