using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrowserGame.Migrations
{
    public partial class ResourceFieldsCityReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "ResourceFields",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceFields_CityId",
                table: "ResourceFields",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceFields_Cities_CityId",
                table: "ResourceFields",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceFields_Cities_CityId",
                table: "ResourceFields");

            migrationBuilder.DropIndex(
                name: "IX_ResourceFields_CityId",
                table: "ResourceFields");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "ResourceFields");
        }
    }
}
