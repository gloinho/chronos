using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chronos.Data.Migrations
{
    public partial class AdicionadoColunaTogglIdEmTarefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TogglId",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TogglId",
                table: "Tarefas");
        }
    }
}
