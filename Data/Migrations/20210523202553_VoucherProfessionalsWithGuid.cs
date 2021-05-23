using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class VoucherProfessionalsWithGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfessionalId",
                table: "VoucherProfessionals");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "VoucherProfessionals",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VoucherProfessionals");

            migrationBuilder.AddColumn<int>(
                name: "ProfessionalId",
                table: "VoucherProfessionals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
