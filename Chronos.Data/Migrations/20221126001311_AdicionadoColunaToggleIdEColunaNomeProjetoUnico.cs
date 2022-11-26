using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chronos.Data.Migrations
{
    public partial class AdicionadoColunaToggleIdEColunaNomeProjetoUnico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TogglId",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Projetos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_Nome",
                table: "Projetos",
                column: "Nome",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projetos_Nome",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "TogglId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Projetos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
