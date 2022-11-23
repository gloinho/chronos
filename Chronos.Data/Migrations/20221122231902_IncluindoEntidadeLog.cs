using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chronos.Data.Migrations
{
    public partial class IncluindoEntidadeLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Usuarios_Projetos");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ResetSenhaVencimento",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "TotalHoras",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Projetos");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Responsavel = table.Column<int>(type: "int", nullable: true),
                    Alteracao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Usuarios_Projetos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Usuarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetSenhaVencimento",
                table: "Usuarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Tarefas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalHoras",
                table: "Tarefas",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Projetos",
                type: "datetime2",
                nullable: true);
        }
    }
}
