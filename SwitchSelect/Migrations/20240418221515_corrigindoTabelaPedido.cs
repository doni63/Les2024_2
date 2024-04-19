using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSelect.Migrations
{
    /// <inheritdoc />
    public partial class corrigindoTabelaPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cartaoId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_cartaoId",
                table: "Pedidos",
                column: "cartaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Cartoes_cartaoId",
                table: "Pedidos",
                column: "cartaoId",
                principalTable: "Cartoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Cartoes_cartaoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_cartaoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "cartaoId",
                table: "Pedidos");
        }
    }
}
