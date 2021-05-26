using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class VoucherProfessionalsTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherProfessionals_Vouchers_VuocherId",
                table: "VoucherProfessionals");

            migrationBuilder.DropIndex(
                name: "IX_VoucherProfessionals_VuocherId",
                table: "VoucherProfessionals");

            migrationBuilder.DropColumn(
                name: "VuocherId",
                table: "VoucherProfessionals");

            migrationBuilder.AddColumn<int>(
                name: "VoucherId",
                table: "VoucherProfessionals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherProfessionals_VoucherId",
                table: "VoucherProfessionals",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherProfessionals_Vouchers_VoucherId",
                table: "VoucherProfessionals",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherProfessionals_Vouchers_VoucherId",
                table: "VoucherProfessionals");

            migrationBuilder.DropIndex(
                name: "IX_VoucherProfessionals_VoucherId",
                table: "VoucherProfessionals");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                table: "VoucherProfessionals");

            migrationBuilder.AddColumn<int>(
                name: "VuocherId",
                table: "VoucherProfessionals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherProfessionals_VuocherId",
                table: "VoucherProfessionals",
                column: "VuocherId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherProfessionals_Vouchers_VuocherId",
                table: "VoucherProfessionals",
                column: "VuocherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
