using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrowserGame.Migrations
{
    public partial class AddedCityCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "XCoord",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YCoord",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XCoord",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "YCoord",
                table: "Cities");
        }
    }
}
