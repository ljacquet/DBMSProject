using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBMSApi.Migrations
{
    public partial class actuallyremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expiredDate",
                table: "roomateIngredients");

            migrationBuilder.DropColumn(
                name: "priceUnit",
                table: "roomateIngredients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "expiredDate",
                table: "roomateIngredients",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "priceUnit",
                table: "roomateIngredients",
                type: "TEXT",
                nullable: true);
        }
    }
}
