using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chronos.Data.Migrations
{
    public partial class foreignkeytarefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Usuario_ProjetoId",
                table: "Usuarios_Projetos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TarefaId",
                table: "Tarefas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProjetoId",
                table: "Projetos",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Usuarios_Projetos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "Usuarios_Projetos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Tarefas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "Tarefas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Projetos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "Projetos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Usuarios_Projetos");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "Usuarios_Projetos");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "Projetos");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuarios_Projetos",
                newName: "Usuario_ProjetoId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuarios",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tarefas",
                newName: "TarefaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Projetos",
                newName: "ProjetoId");
        }
    }
}
