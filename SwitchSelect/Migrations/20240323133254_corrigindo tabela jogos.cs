using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSelect.Migrations
{
    /// <inheritdoc />
    public partial class corrigindotabelajogos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Jogos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Jogos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
