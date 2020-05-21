using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Fulfillment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventId",
                table: "Availabilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventUrl",
                table: "Availabilities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FulfillmentDate",
                table: "Availabilities",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFulfilled",
                table: "Availabilities",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "EventUrl",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "FulfillmentDate",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "IsFulfilled",
                table: "Availabilities");
        }
    }
}
