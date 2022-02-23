using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrowserGame.Data.Migrations
{
    public partial class BasicModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<long>(type: "bigint", nullable: false),
                    Available = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpkeepBuildings = table.Column<int>(type: "int", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<long>(type: "bigint", nullable: false),
                    Available = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Irons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<long>(type: "bigint", nullable: false),
                    Available = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Irons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UpgradeInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClayCost = table.Column<int>(type: "int", nullable: false),
                    WoodCost = table.Column<int>(type: "int", nullable: false),
                    IronCost = table.Column<int>(type: "int", nullable: false),
                    CropCost = table.Column<int>(type: "int", nullable: false),
                    IsFinnalUpgrade = table.Column<bool>(type: "bit", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    AdditionalCropUpkeep = table.Column<int>(type: "int", nullable: false),
                    ValueChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpgradeInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Woods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<long>(type: "bigint", nullable: false),
                    Available = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Woods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClayId = table.Column<int>(type: "int", nullable: false),
                    WoodId = table.Column<int>(type: "int", nullable: false),
                    IronId = table.Column<int>(type: "int", nullable: false),
                    CropId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Clays_ClayId",
                        column: x => x.ClayId,
                        principalTable: "Clays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cities_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cities_Irons_IronId",
                        column: x => x.IronId,
                        principalTable: "Irons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cities_Woods_WoodId",
                        column: x => x.WoodId,
                        principalTable: "Woods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionPerHour = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CropUpkeep = table.Column<int>(type: "int", nullable: false),
                    UpgradeInfoId = table.Column<int>(type: "int", nullable: false),
                    ClayId = table.Column<int>(type: "int", nullable: true),
                    CropId = table.Column<int>(type: "int", nullable: true),
                    IronId = table.Column<int>(type: "int", nullable: true),
                    WoodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceFields_Clays_ClayId",
                        column: x => x.ClayId,
                        principalTable: "Clays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResourceFields_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResourceFields_Irons_IronId",
                        column: x => x.IronId,
                        principalTable: "Irons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResourceFields_UpgradeInfos_UpgradeInfoId",
                        column: x => x.UpgradeInfoId,
                        principalTable: "UpgradeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceFields_Woods_WoodId",
                        column: x => x.WoodId,
                        principalTable: "Woods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CapitalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Cities_CapitalId",
                        column: x => x.CapitalId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ClayId",
                table: "Cities",
                column: "ClayId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CropId",
                table: "Cities",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IronId",
                table: "Cities",
                column: "IronId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_WoodId",
                table: "Cities",
                column: "WoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CapitalId",
                table: "Players",
                column: "CapitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Name",
                table: "Players",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceFields_ClayId",
                table: "ResourceFields",
                column: "ClayId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceFields_CropId",
                table: "ResourceFields",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceFields_IronId",
                table: "ResourceFields",
                column: "IronId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceFields_UpgradeInfoId",
                table: "ResourceFields",
                column: "UpgradeInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceFields_WoodId",
                table: "ResourceFields",
                column: "WoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "ResourceFields");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "UpgradeInfos");

            migrationBuilder.DropTable(
                name: "Clays");

            migrationBuilder.DropTable(
                name: "Crops");

            migrationBuilder.DropTable(
                name: "Irons");

            migrationBuilder.DropTable(
                name: "Woods");
        }
    }
}
