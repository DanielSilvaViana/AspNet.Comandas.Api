﻿// <auto-generated />
using Comandas.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Comandas.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241107003158_Create_Database")]
    partial class Create_Database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Comandas.Api.Models.CardapioItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PossuiPreparo")
                        .HasColumnType("bit");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CardapioItems");
                });

            modelBuilder.Entity("Comandas.Api.Models.Comanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NomeCliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroMesa")
                        .HasColumnType("int");

                    b.Property<int>("SituacaoComanda")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Comandas");
                });

            modelBuilder.Entity("Comandas.Api.Models.ComandaItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardapioItemId")
                        .HasColumnType("int");

                    b.Property<int>("ComandaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardapioItemId");

                    b.HasIndex("ComandaId");

                    b.ToTable("ComandaItems");
                });

            modelBuilder.Entity("Comandas.Api.Models.Mesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NumeroMesa")
                        .HasColumnType("int");

                    b.Property<int>("SituacaoMesa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Mesa");
                });

            modelBuilder.Entity("Comandas.Api.Models.PedidoCozinha", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ComandaId")
                        .HasColumnType("int");

                    b.Property<int>("SituacaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComandaId");

                    b.ToTable("PedidoCozinha");
                });

            modelBuilder.Entity("Comandas.Api.Models.PedidoCozinhaItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ComandaItemId")
                        .HasColumnType("int");

                    b.Property<int>("PedidoCozinhaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComandaItemId");

                    b.HasIndex("PedidoCozinhaId");

                    b.ToTable("PedidoCozinhaItem");
                });

            modelBuilder.Entity("Comandas.Api.Models.ComandaItem", b =>
                {
                    b.HasOne("Comandas.Api.Models.CardapioItem", "CardapioItem")
                        .WithMany()
                        .HasForeignKey("CardapioItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Comandas.Api.Models.Comanda", "Comanda")
                        .WithMany("ComandaItems")
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardapioItem");

                    b.Navigation("Comanda");
                });

            modelBuilder.Entity("Comandas.Api.Models.PedidoCozinha", b =>
                {
                    b.HasOne("Comandas.Api.Models.Comanda", "Comanda")
                        .WithMany()
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comanda");
                });

            modelBuilder.Entity("Comandas.Api.Models.PedidoCozinhaItem", b =>
                {
                    b.HasOne("Comandas.Api.Models.ComandaItem", "ComandaItem")
                        .WithMany()
                        .HasForeignKey("ComandaItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Comandas.Api.Models.PedidoCozinha", "PedidoCozinha")
                        .WithMany("PedidoCozinhaItens")
                        .HasForeignKey("PedidoCozinhaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ComandaItem");

                    b.Navigation("PedidoCozinha");
                });

            modelBuilder.Entity("Comandas.Api.Models.Comanda", b =>
                {
                    b.Navigation("ComandaItems");
                });

            modelBuilder.Entity("Comandas.Api.Models.PedidoCozinha", b =>
                {
                    b.Navigation("PedidoCozinhaItens");
                });
#pragma warning restore 612, 618
        }
    }
}
