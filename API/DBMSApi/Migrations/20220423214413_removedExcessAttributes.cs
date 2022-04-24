using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBMSApi.Migrations
{
    public partial class removedExcessAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estimatedPrice",
                table: "ingredients");

            migrationBuilder.DropColumn(
                name: "substituteNames",
                table: "ingredients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "estimatedPrice",
                table: "ingredients",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "substituteNames",
                table: "ingredients",
                type: "TEXT",
                nullable: true);
        }
    }
}
