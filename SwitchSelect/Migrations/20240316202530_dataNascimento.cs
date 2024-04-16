using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSelect.Migrations
{
    /// <inheritdoc />
    public partial class dataNascimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteViewModel",
                table: "ClienteViewModel");

            migrationBuilder.RenameTable(
                name: "ClienteViewModel",
                newName: "ClienteViewModels");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeNascimento",
                table: "Clientes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeNascimento",
                table: "ClienteViewModels",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteViewModels",
                table: "ClienteViewModels",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteViewModels",
                table: "ClienteViewModels");

            migrationBuilder.DropColumn(
                name: "DataDeNascimento",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DataDeNascimento",
                table: "ClienteViewModels");

            migrationBuilder.RenameTable(
                name: "ClienteViewModels",
                newName: "ClienteViewModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteViewModel",
                table: "ClienteViewModel",
                column: "Id");
        }
    }
}
