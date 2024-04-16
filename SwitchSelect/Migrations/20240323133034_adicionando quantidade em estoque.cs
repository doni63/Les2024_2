using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSelect.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoquantidadeemestoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Jogos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Jogos");
        }
    }
}
