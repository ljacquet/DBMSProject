using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBMSApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "houses",
                columns: table => new
                {
                    houseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    houseName = table.Column<string>(type: "TEXT", nullable: false),
                    ownerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_houses", x => x.houseId);
                });

            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    ingredientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ingredientName = table.Column<string>(type: "TEXT", nullable: false),
                    substituteNames = table.Column<string>(type: "TEXT", nullable: true),
                    estimatedPrice = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.ingredientId);
                });

            migrationBuilder.CreateTable(
                name: "recipes",
                columns: table => new
                {
                    recipeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    recipeName = table.Column<string>(type: "TEXT", nullable: false),
                    link = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipes", x => x.recipeId);
                });

            migrationBuilder.CreateTable(
                name: "roomates",
                columns: table => new
                {
                    roomateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fName = table.Column<string>(type: "TEXT", nullable: false),
                    lName = table.Column<string>(type: "TEXT", nullable: false),
                    role = table.Column<int>(type: "INTEGER", nullable: false),
                    houseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roomates", x => x.roomateId);
                    table.ForeignKey(
                        name: "FK_roomates_houses_houseId",
                        column: x => x.houseId,
                        principalTable: "houses",
                        principalColumn: "houseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recipeIngredients",
                columns: table => new
                {
                    recipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ingredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    ingredientAmount = table.Column<double>(type: "REAL", nullable: false),
                    ingredientUnit = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipeIngredients", x => new { x.ingredientId, x.recipeId });
                    table.ForeignKey(
                        name: "FK_recipeIngredients_ingredients_ingredientId",
                        column: x => x.ingredientId,
                        principalTable: "ingredients",
                        principalColumn: "ingredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recipeIngredients_recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roomateIngredients",
                columns: table => new
                {
                    ingredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    roomateId = table.Column<int>(type: "INTEGER", nullable: false),
                    quantity = table.Column<double>(type: "REAL", nullable: false),
                    quantityUnit = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<double>(type: "REAL", nullable: false),
                    priceUnit = table.Column<string>(type: "TEXT", nullable: false),
                    expiredDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roomateIngredients", x => new { x.roomateId, x.ingredientId });
                    table.ForeignKey(
                        name: "FK_roomateIngredients_ingredients_ingredientId",
                        column: x => x.ingredientId,
                        principalTable: "ingredients",
                        principalColumn: "ingredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roomateIngredients_roomates_roomateId",
                        column: x => x.roomateId,
                        principalTable: "roomates",
                        principalColumn: "roomateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recipeIngredients_recipeId",
                table: "recipeIngredients",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_roomateIngredients_ingredientId",
                table: "roomateIngredients",
                column: "ingredientId");

            migrationBuilder.CreateIndex(
                name: "IX_roomates_houseId",
                table: "roomates",
                column: "houseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipeIngredients");

            migrationBuilder.DropTable(
                name: "roomateIngredients");

            migrationBuilder.DropTable(
                name: "recipes");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropTable(
                name: "roomates");

            migrationBuilder.DropTable(
                name: "houses");
        }
    }
}
