using Comandas.Api.Data;
using Comandas.Api.Models;

namespace Comandas.Api
{
    public static class InicializarDados
    {
        public static void Semear(AppDbContext appDbContext)
        {
            if (!appDbContext.Usuarios.Any())
            {
                appDbContext.Usuarios.Add(new Models.Usuario
                { Name = "Admin", Email = "admin@admin.com", Senha = "admin" });
                appDbContext.SaveChanges();
            }

            if (!appDbContext.CardapioItems.Any())
            {
                appDbContext.CardapioItems.AddRange(
                    new Models.CardapioItem
                    {
                        Descricao = "XIS SALADA , BIFE, OVO, PRESUNTO, QUEIJO",
                        PossuiPreparo = true,
                        Preco = 20.00M,
                        Titulo = "XIS SALADA"
                    },
                      new CardapioItem()
                      {
                          Descricao = "XIS BACON, BIFE, OVO, BACON, ALFACE, CEBOLA",
                          PossuiPreparo = true,
                          Preco = 15M,
                          Titulo = "XIS BACON"
                      },
                      new CardapioItem
                      {
                          Descricao = "COCA COLA LATA 350ML ",
                          PossuiPreparo = false,
                          Preco = 9M,
                          Titulo = "COCA COLA LATA 350 ML"
                      });

            }
            if (!appDbContext.Mesas.Any())
            {
                appDbContext.Mesas.AddRange(
                    new Models.Mesa
                    {
                        NumeroMesa = 1,
                        SituacaoMesa = 0

                    },
                    new Mesa
                    {
                        NumeroMesa = 2,
                        SituacaoMesa = 0
                    },
                     new Mesa
                     {
                         NumeroMesa = 3,
                         SituacaoMesa = 0
                     },
                      new Mesa
                      {
                          NumeroMesa = 4,
                          SituacaoMesa = 0
                      });
            }

            if (!appDbContext.Comandas.Any())
            {
                var comanda = new Comanda()
                {
                    NomeCliente = "Daniel Silva",
                    NumeroMesa = 1,
                    SituacaoComanda = 1
                };
                appDbContext.Comandas.Add(comanda);

                ComandaItem[] comandaItems = { new ComandaItem { Comanda = comanda, CardapioItemId = 1 }, new ComandaItem { Comanda = comanda, CardapioItemId = 2 } };

                if (!appDbContext.ComandaItems.Any())
                {
                    appDbContext.ComandaItems.AddRange(comandaItems);
                }


                var pedidoCozinha = new PedidoCozinha()
                {
                    Comanda = comanda
                };

                var pedidoCozinha2 = new PedidoCozinha()
                {
                    Comanda = comanda
                };
                PedidoCozinhaItem[] pedidoCozinhaItems =
                    { new PedidoCozinhaItem {
                          PedidoCozinha = pedidoCozinha, ComandaItem = comandaItems[0]              
                    },
                    new PedidoCozinhaItem {
                        PedidoCozinha = pedidoCozinha2, ComandaItem = comandaItems[1]
                    }
                
                };

                appDbContext.PedidoCozinhas.AddRange(pedidoCozinha, pedidoCozinha2);
                appDbContext.PedidoCozinhaItems.AddRange(pedidoCozinhaItems);
            }
            appDbContext.SaveChanges();
        }
    }
}
