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
        }
    }
}
