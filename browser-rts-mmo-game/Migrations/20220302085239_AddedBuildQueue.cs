using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrowserGame.Migrations
{
    public partial class AddedBuildQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueChange",
                table: "UpgradeInfos",
                newName: "ValueChangeDecimal");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ResourceFields",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "UpgradeDuration",
                table: "UpgradeInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpgradeInProgress",
                table: "ResourceFields",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BuildQueues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TargetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetId = table.Column<int>(type: "int", nullable: true),
                    TargetLevel = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    BuildingType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildQueues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildQueues_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildQueues_CityId",
                table: "BuildQueues",
                column: "CityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildQueues");

            migrationBuilder.DropColumn(
                name: "UpgradeDuration",
                table: "UpgradeInfos");

            migrationBuilder.DropColumn(
                name: "IsUpgradeInProgress",
                table: "ResourceFields");

            migrationBuilder.RenameColumn(
                name: "ValueChangeDecimal",
                table: "UpgradeInfos",
                newName: "ValueChange");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ResourceFields",
                newName: "Type");
        }
    }
}
