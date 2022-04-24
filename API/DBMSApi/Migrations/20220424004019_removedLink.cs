using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBMSApi.Migrations
{
    public partial class removedLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link",
                table: "recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "link",
                table: "recipes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
