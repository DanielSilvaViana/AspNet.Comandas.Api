using Comandas.Data.Interfaces;
using Comandas.Services.Interfaces;
using Comandas.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Testes
{
    public class ComandaServicesTest
    {
        private readonly Mock<IComandaRepository> _comandaRepository;
        private readonly Mock<IMesaRepository> _mesaRepository;
        private readonly Mock<IComandaItemRepository> _comandaItemRepository;
        private readonly Mock<ICardapioItemRepository> _cardapioItemRepository;
        private readonly Mock<IPedidoCozinhaRepository> _pedidoRepository;
        private readonly Mock<IPedidoCozinhaItemRepository> _pedidoCozinhaItemRepository;
        private readonly IComandaServices _comandaServices;


        public ComandaServicesTest()
        {
            _mesaRepository = new Mock<IMesaRepository>();
            _comandaRepository = new Mock<IComandaRepository>();
            _comandaItemRepository = new Mock<IComandaItemRepository>();
            _cardapioItemRepository = new Mock<ICardapioItemRepository>();
            _pedidoRepository = new Mock<IPedidoCozinhaRepository>();
            _pedidoCozinhaItemRepository = new Mock<IPedidoCozinhaItemRepository>();
            //_comandaServices = new ComandaServices(_mesaRepository.Object);
        }
    }
}
