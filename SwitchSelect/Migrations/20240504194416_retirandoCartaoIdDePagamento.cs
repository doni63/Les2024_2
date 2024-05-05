using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSelect.Migrations
{
    /// <inheritdoc />
    public partial class retirandoCartaoIdDePagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PagamentosCartoes_Pagamentos_PagamentoId",
                table: "PagamentosCartoes");

            migrationBuilder.DropColumn(
                name: "Cartaoid",
                table: "Pagamentos");

            migrationBuilder.AlterColumn<int>(
                name: "PagamentoId",
                table: "PagamentosCartoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PagamentosCartoes_Pagamentos_PagamentoId",
                table: "PagamentosCartoes",
                column: "PagamentoId",
                principalTable: "Pagamentos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PagamentosCartoes_Pagamentos_PagamentoId",
                table: "PagamentosCartoes");

            migrationBuilder.AlterColumn<int>(
                name: "PagamentoId",
                table: "PagamentosCartoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cartaoid",
                table: "Pagamentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PagamentosCartoes_Pagamentos_PagamentoId",
                table: "PagamentosCartoes",
                column: "PagamentoId",
                principalTable: "Pagamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
