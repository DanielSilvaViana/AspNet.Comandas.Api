using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comandas.Api.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Tables_For_Plural : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoCozinha_Comandas_ComandaId",
                table: "PedidoCozinha");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoCozinhaItem_ComandaItems_ComandaItemId",
                table: "PedidoCozinhaItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoCozinhaItem_PedidoCozinha_PedidoCozinhaId",
                table: "PedidoCozinhaItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoCozinhaItem",
                table: "PedidoCozinhaItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoCozinha",
                table: "PedidoCozinha");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mesa",
                table: "Mesa");

            migrationBuilder.RenameTable(
                name: "PedidoCozinhaItem",
                newName: "PedidoCozinhaItems");

            migrationBuilder.RenameTable(
                name: "PedidoCozinha",
                newName: "PedidoCozinhas");

            migrationBuilder.RenameTable(
                name: "Mesa",
                newName: "Mesas");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoCozinhaItem_PedidoCozinhaId",
                table: "PedidoCozinhaItems",
                newName: "IX_PedidoCozinhaItems_PedidoCozinhaId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoCozinhaItem_ComandaItemId",
                table: "PedidoCozinhaItems",
                newName: "IX_PedidoCozinhaItems_ComandaItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoCozinha_ComandaId",
                table: "PedidoCozinhas",
                newName: "IX_PedidoCozinhas_ComandaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoCozinhaItems",
                table: "PedidoCozinhaItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoCozinhas",
                table: "PedidoCozinhas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mesas",
                table: "Mesas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoCozinhaItems_ComandaItems_ComandaItemId",
                table: "PedidoCozinhaItems",
                column: "ComandaItemId",
                principalTable: "ComandaItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoCozinhaItems_PedidoCozinhas_PedidoCozinhaId",
                table: "PedidoCozinhaItems",
                column: "PedidoCozinhaId",
                principalTable: "PedidoCozinhas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoCozinhas_Comandas_ComandaId",
                table: "PedidoCozinhas",
                column: "ComandaId",
                principalTable: "Comandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoCozinhaItems_ComandaItems_ComandaItemId",
                table: "PedidoCozinhaItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoCozinhaItems_PedidoCozinhas_PedidoCozinhaId",
                table: "PedidoCozinhaItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoCozinhas_Comandas_ComandaId",
                table: "PedidoCozinhas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoCozinhas",
                table: "PedidoCozinhas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoCozinhaItems",
                table: "PedidoCozinhaItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mesas",
                table: "Mesas");

            migrationBuilder.RenameTable(
                name: "PedidoCozinhas",
                newName: "PedidoCozinha");

            migrationBuilder.RenameTable(
                name: "PedidoCozinhaItems",
                newName: "PedidoCozinhaItem");

            migrationBuilder.RenameTable(
                name: "Mesas",
                newName: "Mesa");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoCozinhas_ComandaId",
                table: "PedidoCozinha",
                newName: "IX_PedidoCozinha_ComandaId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoCozinhaItems_PedidoCozinhaId",
                table: "PedidoCozinhaItem",
                newName: "IX_PedidoCozinhaItem_PedidoCozinhaId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoCozinhaItems_ComandaItemId",
                table: "PedidoCozinhaItem",
                newName: "IX_PedidoCozinhaItem_ComandaItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoCozinha",
                table: "PedidoCozinha",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoCozinhaItem",
                table: "PedidoCozinhaItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mesa",
                table: "Mesa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoCozinha_Comandas_ComandaId",
                table: "PedidoCozinha",
                column: "ComandaId",
                principalTable: "Comandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoCozinhaItem_ComandaItems_ComandaItemId",
                table: "PedidoCozinhaItem",
                column: "ComandaItemId",
                principalTable: "ComandaItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoCozinhaItem_PedidoCozinha_PedidoCozinhaId",
                table: "PedidoCozinhaItem",
                column: "PedidoCozinhaId",
                principalTable: "PedidoCozinha",
                principalColumn: "Id");
        }
    }
}
