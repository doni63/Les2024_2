using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSelect.Migrations
{
    /// <inheritdoc />
    public partial class atualizandoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Cartoes_CartaoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_CartaoId",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Cartoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cartoes_PedidoId",
                table: "Cartoes",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartoes_Pedidos_PedidoId",
                table: "Cartoes",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartoes_Pedidos_PedidoId",
                table: "Cartoes");

            migrationBuilder.DropIndex(
                name: "IX_Cartoes_PedidoId",
                table: "Cartoes");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Cartoes");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CartaoId",
                table: "Pedidos",
                column: "CartaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Cartoes_CartaoId",
                table: "Pedidos",
                column: "CartaoId",
                principalTable: "Cartoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
