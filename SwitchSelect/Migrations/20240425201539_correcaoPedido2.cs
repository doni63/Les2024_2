using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSelect.Migrations
{
    /// <inheritdoc />
    public partial class correcaoPedido2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Bandeira",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CVV",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CpfTitularCartao",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "DDD",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "DataDeNascimento",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "DataValidade",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "NumeroCartao",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "NumeroTelefone",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "RG",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "TitularDoCartao",
                table: "Pedidos");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EnderecoId",
                table: "Pedidos",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Enderecos_EnderecoId",
                table: "Pedidos",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Enderecos_EnderecoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_EnderecoId",
                table: "Pedidos");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Pedidos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Bandeira",
                table: "Pedidos",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Pedidos",
                type: "varchar(8)",
                maxLength: 8,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CVV",
                table: "Pedidos",
                type: "varchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Pedidos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Pedidos",
                type: "varchar(11)",
                maxLength: 11,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CpfTitularCartao",
                table: "Pedidos",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DDD",
                table: "Pedidos",
                type: "varchar(3)",
                maxLength: 3,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeNascimento",
                table: "Pedidos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataValidade",
                table: "Pedidos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Pedidos",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Pedidos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Pedidos",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "Pedidos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Pedidos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Pedidos",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NumeroCartao",
                table: "Pedidos",
                type: "varchar(16)",
                maxLength: 16,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NumeroTelefone",
                table: "Pedidos",
                type: "varchar(9)",
                maxLength: 9,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Pedidos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RG",
                table: "Pedidos",
                type: "varchar(9)",
                maxLength: 9,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TitularDoCartao",
                table: "Pedidos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
