using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSelect.Migrations
{
    /// <inheritdoc />
    public partial class classePagamentoCartao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartoes_Pagamentos_PagamentoId",
                table: "Cartoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cupons_Pagamentos_PagamentoId",
                table: "Cupons");

            migrationBuilder.DropIndex(
                name: "IX_Cupons_PagamentoId",
                table: "Cupons");

            migrationBuilder.DropIndex(
                name: "IX_Cartoes_PagamentoId",
                table: "Cartoes");

            migrationBuilder.DropColumn(
                name: "PagamentoId",
                table: "Cupons");

            migrationBuilder.DropColumn(
                name: "PagamentoId",
                table: "Cartoes");

            migrationBuilder.AddColumn<string>(
                name: "CartaoIds",
                table: "Pagamentos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CodigosCupons",
                table: "Pagamentos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartaoIds",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "CodigosCupons",
                table: "Pagamentos");

            migrationBuilder.AddColumn<int>(
                name: "PagamentoId",
                table: "Cupons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PagamentoId",
                table: "Cartoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cupons_PagamentoId",
                table: "Cupons",
                column: "PagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartoes_PagamentoId",
                table: "Cartoes",
                column: "PagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartoes_Pagamentos_PagamentoId",
                table: "Cartoes",
                column: "PagamentoId",
                principalTable: "Pagamentos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupons_Pagamentos_PagamentoId",
                table: "Cupons",
                column: "PagamentoId",
                principalTable: "Pagamentos",
                principalColumn: "Id");
        }
    }
}
