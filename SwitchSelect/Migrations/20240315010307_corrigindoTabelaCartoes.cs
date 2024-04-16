using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSelect.Migrations
{
    /// <inheritdoc />
    public partial class corrigindoTabelaCartoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartões_Clientes_ClienteId",
                table: "Cartões");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cartões",
                table: "Cartões");

            migrationBuilder.RenameTable(
                name: "Cartões",
                newName: "Cartoes");

            migrationBuilder.RenameIndex(
                name: "IX_Cartões_ClienteId",
                table: "Cartoes",
                newName: "IX_Cartoes_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cartoes",
                table: "Cartoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartoes_Clientes_ClienteId",
                table: "Cartoes",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartoes_Clientes_ClienteId",
                table: "Cartoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cartoes",
                table: "Cartoes");

            migrationBuilder.RenameTable(
                name: "Cartoes",
                newName: "Cartões");

            migrationBuilder.RenameIndex(
                name: "IX_Cartoes_ClienteId",
                table: "Cartões",
                newName: "IX_Cartões_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cartões",
                table: "Cartões",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartões_Clientes_ClienteId",
                table: "Cartões",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
