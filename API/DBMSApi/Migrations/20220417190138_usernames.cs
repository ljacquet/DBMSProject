using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBMSApi.Migrations
{
    public partial class usernames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fName",
                table: "roomates");

            migrationBuilder.RenameColumn(
                name: "lName",
                table: "roomates",
                newName: "username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "roomates",
                newName: "lName");

            migrationBuilder.AddColumn<string>(
                name: "fName",
                table: "roomates",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
