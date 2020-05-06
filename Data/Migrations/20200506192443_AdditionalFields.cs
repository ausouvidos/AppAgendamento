using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AdditionalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "Availabilities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Availabilities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Availabilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "Availabilities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedDate",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "Observations",
                table: "Availabilities");
        }
    }
}
