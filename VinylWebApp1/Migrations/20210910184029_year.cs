using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylWebApp1.Migrations
{
    public partial class year : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Vinyls");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Vinyls",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Vinyls");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Vinyls",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
