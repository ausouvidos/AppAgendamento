using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class VoucherProfessionals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VoucherProfessionals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VuocherId = table.Column<int>(nullable: false),
                    ProfessionalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherProfessionals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherProfessionals_Vouchers_VuocherId",
                        column: x => x.VuocherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherProfessionals_VuocherId",
                table: "VoucherProfessionals",
                column: "VuocherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherProfessionals");
        }
    }
}
