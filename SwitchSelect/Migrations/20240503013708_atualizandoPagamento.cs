using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSelect.Migrations
{
    /// <inheritdoc />
    public partial class atualizandoPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartoes_Pedidos_PedidoId",
                table: "Cartoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Cupons_CupomId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_CupomId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Cartoes_PedidoId",
                table: "Cartoes");

            migrationBuilder.DropColumn(
                name: "CartaoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CupomId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "NumeroCartao",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Cartoes");

            migrationBuilder.AddColumn<string>(
                name: "NumerosCartao",
                table: "Pagamentos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PagamentoId",
                table: "Cupons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cupons_PagamentoId",
                table: "Cupons",
                column: "PagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupons_Pagamentos_PagamentoId",
                table: "Cupons",
                column: "PagamentoId",
                principalTable: "Pagamentos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cupons_Pagamentos_PagamentoId",
                table: "Cupons");

            migrationBuilder.DropIndex(
                name: "IX_Cupons_PagamentoId",
                table: "Cupons");

            migrationBuilder.DropColumn(
                name: "NumerosCartao",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "PagamentoId",
                table: "Cupons");

            migrationBuilder.AddColumn<int>(
                name: "CartaoId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CupomId",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Desconto",
                table: "Pedidos",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroCartao",
                table: "Pagamentos",
                type: "varchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Cartoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CupomId",
                table: "Pedidos",
                column: "CupomId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Cupons_CupomId",
                table: "Pedidos",
                column: "CupomId",
                principalTable: "Cupons",
                principalColumn: "Id");
        }
    }
}
