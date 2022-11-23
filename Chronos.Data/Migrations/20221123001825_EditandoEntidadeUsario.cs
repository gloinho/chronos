using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chronos.Data.Migrations
{
    public partial class EditandoEntidadeUsario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetSenhaToken",
                table: "Usuarios",
                newName: "CodigoSenhaToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodigoSenhaToken",
                table: "Usuarios",
                newName: "ResetSenhaToken");
        }
    }
}
