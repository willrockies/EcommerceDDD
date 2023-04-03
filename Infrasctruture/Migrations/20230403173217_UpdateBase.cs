using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrasctruture.Migrations
{
    public partial class UpdateBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PRD_URL",
                table: "TB_Produto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PRD_URL",
                table: "TB_Produto");
        }
    }
}
