using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chronos.Data.Migrations
{
    public partial class AlterandoEntidadeLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Usuarios_AlteradorId",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "Mensagem",
                table: "Logs",
                newName: "Alteracao");

            migrationBuilder.RenameColumn(
                name: "AlteradorId",
                table: "Logs",
                newName: "ResponsavelId");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_AlteradorId",
                table: "Logs",
                newName: "IX_Logs_ResponsavelId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Logs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Usuarios_ResponsavelId",
                table: "Logs",
                column: "ResponsavelId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Usuarios_ResponsavelId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "ResponsavelId",
                table: "Logs",
                newName: "AlteradorId");

            migrationBuilder.RenameColumn(
                name: "Alteracao",
                table: "Logs",
                newName: "Mensagem");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_ResponsavelId",
                table: "Logs",
                newName: "IX_Logs_AlteradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Usuarios_AlteradorId",
                table: "Logs",
                column: "AlteradorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
