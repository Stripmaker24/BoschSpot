using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoschSpot.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                },
                comment: "Accounts of the website");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                },
                comment: "The different types of categories that a group can be part of");

            migrationBuilder.CreateTable(
                name: "Rarities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rarity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rarities", x => x.ID);
                },
                comment: "Rarity of the contenders");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                    table.ForeignKey(
                        name: "ForeignKey_Category_Group",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Groups that a contender can be part of");

            migrationBuilder.CreateTable(
                name: "Contenders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsInDanger = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recognition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RarityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contenders", x => x.ID);
                    table.ForeignKey(
                        name: "ForeignKey_Group_Contender",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_Rarity_Contender",
                        column: x => x.RarityID,
                        principalTable: "Rarities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "The entities that can be spotted in the game");

            migrationBuilder.CreateTable(
                name: "Spot",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContenderID = table.Column<int>(type: "int", nullable: false),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Long = table.Column<double>(type: "float", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    SpotTimeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spot", x => x.ID);
                    table.ForeignKey(
                        name: "ForeignKey_Account_Spot",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_Contender_Spot",
                        column: x => x.ContenderID,
                        principalTable: "Contenders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contenders_GroupID",
                table: "Contenders",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Contenders_RarityID",
                table: "Contenders",
                column: "RarityID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CategoryID",
                table: "Groups",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Spot_AccountID",
                table: "Spot",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Spot_ContenderID",
                table: "Spot",
                column: "ContenderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spot");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Contenders");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Rarities");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
