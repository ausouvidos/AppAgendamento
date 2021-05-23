using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class VoucherProfessionalsUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VoucherProfessionals_UserId",
                table: "VoucherProfessionals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherProfessionals_Users_UserId",
                table: "VoucherProfessionals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherProfessionals_Users_UserId",
                table: "VoucherProfessionals");

            migrationBuilder.DropIndex(
                name: "IX_VoucherProfessionals_UserId",
                table: "VoucherProfessionals");
        }
    }
}
