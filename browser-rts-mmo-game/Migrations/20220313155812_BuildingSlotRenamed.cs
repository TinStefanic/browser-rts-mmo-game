using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrowserGame.Migrations
{
    public partial class BuildingSlotRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_BuildSlots_BuildingSlotId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_CityBuildings_BuildSlots_BuildingSlotId",
                table: "CityBuildings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildSlots",
                table: "BuildSlots");

            migrationBuilder.RenameTable(
                name: "BuildSlots",
                newName: "BuildingSlots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingSlots",
                table: "BuildingSlots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_BuildingSlots_BuildingSlotId",
                table: "Cities",
                column: "BuildingSlotId",
                principalTable: "BuildingSlots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CityBuildings_BuildingSlots_BuildingSlotId",
                table: "CityBuildings",
                column: "BuildingSlotId",
                principalTable: "BuildingSlots",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_BuildingSlots_BuildingSlotId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_CityBuildings_BuildingSlots_BuildingSlotId",
                table: "CityBuildings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingSlots",
                table: "BuildingSlots");

            migrationBuilder.RenameTable(
                name: "BuildingSlots",
                newName: "BuildSlots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildSlots",
                table: "BuildSlots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_BuildSlots_BuildingSlotId",
                table: "Cities",
                column: "BuildingSlotId",
                principalTable: "BuildSlots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CityBuildings_BuildSlots_BuildingSlotId",
                table: "CityBuildings",
                column: "BuildingSlotId",
                principalTable: "BuildSlots",
                principalColumn: "Id");
        }
    }
}
