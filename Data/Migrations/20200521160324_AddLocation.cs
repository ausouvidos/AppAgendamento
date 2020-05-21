using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerCity",
                table: "Availabilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerCountry",
                table: "Availabilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerRegion",
                table: "Availabilities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCity",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "CustomerCountry",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "CustomerRegion",
                table: "Availabilities");
        }
    }
}
