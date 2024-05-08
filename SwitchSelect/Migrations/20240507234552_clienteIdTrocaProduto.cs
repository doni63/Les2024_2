using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSelect.Migrations
{
    /// <inheritdoc />
    public partial class clienteIdTrocaProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "TrocaProdutos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "PedidoDetalhes",
                keyColumn: "Restricao",
                keyValue: null,
                column: "Restricao",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Restricao",
                table: "PedidoDetalhes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TrocaProdutos_ClienteId",
                table: "TrocaProdutos",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrocaProdutos_Clientes_ClienteId",
                table: "TrocaProdutos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrocaProdutos_Clientes_ClienteId",
                table: "TrocaProdutos");

            migrationBuilder.DropIndex(
                name: "IX_TrocaProdutos_ClienteId",
                table: "TrocaProdutos");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "TrocaProdutos");

            migrationBuilder.AlterColumn<string>(
                name: "Restricao",
                table: "PedidoDetalhes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
