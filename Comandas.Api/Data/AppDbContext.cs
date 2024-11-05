using Comandas.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CardapioItem> CardapioItems { get; set; }
        public DbSet<Comanda> Comandas { get; set; }

        public DbSet<ComandaItem> ComandaItems { get; set; }

        public DbSet<PedidoCozinha> PedidoCozinha { get; set; }

        public DbSet<PedidoCozinhaItem> PedidoCozinhaItem { get; set; }

        public DbSet<Mesa> Mesa { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Uma Comanda possui muitos ComandaItems
            // E sua chave extrangeira é ComandaId
            modelBuilder.Entity<Comanda>()
                .HasMany<ComandaItem>()
                .WithOne(ci => ci.Comanda)
                .HasForeignKey(f => f.ComandaId);

            modelBuilder.Entity<ComandaItem>()
                .HasOne(ci => ci.Comanda)
                .WithMany(ci => ci.ComandaItems)
                .HasForeignKey(f => f.ComandaId);

            // O Item da comanda possui um Item de Cardápio
            // E sua chave extrangeira é CardapioItemId
            modelBuilder.Entity<ComandaItem>()
                .HasOne(ci => ci.CardapioItem)
                .WithMany()
                .HasForeignKey(ci => ci.CardapioItemId);


            modelBuilder.Entity<PedidoCozinha>()
                .HasMany<PedidoCozinhaItem>()
                .WithOne(ci => ci.PedidoCozinha)
                .HasForeignKey(f => f.PedidoCozinhaId);

            modelBuilder.Entity<PedidoCozinhaItem>()
                .HasOne(ci => ci.PedidoCozinha)
                .WithMany(ci => ci.PedidoCozinhaItem)
                .HasForeignKey(f => f.PedidoCozinhaId);

            // O Item da comanda possui um Item de Cardápio
            // E sua chave extrangeira é CardapioItemId
           // modelBuilder.Entity<Mesa>()
           //.HasMany<Mesa>()
           //.WithOne(ci => ci.SituacaoMesa)
           //.HasForeignKey(f => f.ComandaId);



            base.OnModelCreating(modelBuilder);
        }
    }
}
