using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoWorkshop.Web.Migrations
{
    public partial class addedSpecialty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Mecanics");

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "Mecanics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mecanics_SpecialtyId",
                table: "Mecanics",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mecanics_Specialty_SpecialtyId",
                table: "Mecanics",
                column: "SpecialtyId",
                principalTable: "Specialty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mecanics_Specialty_SpecialtyId",
                table: "Mecanics");

            migrationBuilder.DropTable(
                name: "Specialty");

            migrationBuilder.DropIndex(
                name: "IX_Mecanics_SpecialtyId",
                table: "Mecanics");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Mecanics");

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Mecanics",
                nullable: true);
        }
    }
}
