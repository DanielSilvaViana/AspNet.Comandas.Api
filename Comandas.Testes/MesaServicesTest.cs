using Comandas.Data.Interfaces;
using Comandas.Services.Interfaces;
using Comandas.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Comandas.Data.Repository;
using Comandas.Domain.Models;

namespace Comandas.Testes
{
    public class MesaServicesTest
    {

        private readonly Mock<ILogger<MesaService>> _logger;
        private readonly Mock<IMesaRepository> _mesaRepository;
        private readonly Mock<IComandaServices> _comandaServices;
        private readonly IMesaServices _mesaServices;

        public MesaServicesTest()
        {
            _logger = new Mock<ILogger<MesaService>>();
            _mesaRepository = new Mock<IMesaRepository>();
            _comandaServices = new Mock<IComandaServices>();
            _mesaServices = new MesaService(_logger.Object, _mesaRepository.Object, _comandaServices.Object);
        }


        [Fact]
        public async Task GetMesaByIdSucess()
        {
            //Arrange
            _mesaRepository.Setup(m => m.GetMesaByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Shared.Dtos.MesaDto { Id = 1, NumeroMesa = 1, SituacaoMesa = 1 });
            //Act
            var mesaTeste = await _mesaServices.GetMesaById(1);
            //Assert
            mesaTeste.Should().NotBeNull();
            _mesaRepository.Verify(m => m.GetMesaByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteMesaByIdSucess()
        {
            //Arrange
            _mesaRepository.Setup(m => m.DeleteMesaAsync(It.IsAny<int>()))
                .ReturnsAsync(new Mesa { Id = 1, NumeroMesa = 1, SituacaoMesa = 1 });
            //Act
            var mesaTeste = await _mesaServices.DeleteMesa(1);
            //Assert
            mesaTeste.Should().NotBeNull();
            _mesaRepository.Verify(m => m.DeleteMesaAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
