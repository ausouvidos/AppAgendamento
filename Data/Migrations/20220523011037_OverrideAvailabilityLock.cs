using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class OverrideAvailabilityLock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OverrideAvailabilityLock",
                table: "Vouchers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OverrideAvailabilityLock",
                table: "Vouchers");
        }
    }
}
