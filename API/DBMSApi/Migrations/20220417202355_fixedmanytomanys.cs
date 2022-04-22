using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBMSApi.Migrations
{
    public partial class fixedmanytomanys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_recipeIngredients",
                table: "recipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_recipeIngredients_recipeId",
                table: "recipeIngredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipeIngredients",
                table: "recipeIngredients",
                columns: new[] { "recipeId", "ingredientId" });

            migrationBuilder.CreateIndex(
                name: "IX_recipeIngredients_ingredientId",
                table: "recipeIngredients",
                column: "ingredientId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_roomateId",
                table: "AspNetUsers",
                column: "roomateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_roomates_roomateId",
                table: "AspNetUsers",
                column: "roomateId",
                principalTable: "roomates",
                principalColumn: "roomateId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_roomates_roomateId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipeIngredients",
                table: "recipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_recipeIngredients_ingredientId",
                table: "recipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_roomateId",
                table: "AspNetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipeIngredients",
                table: "recipeIngredients",
                columns: new[] { "ingredientId", "recipeId" });

            migrationBuilder.CreateIndex(
                name: "IX_recipeIngredients_recipeId",
                table: "recipeIngredients",
                column: "recipeId");
        }
    }
}
