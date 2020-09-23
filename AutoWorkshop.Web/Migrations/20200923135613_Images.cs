using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoWorkshop.Web.Migrations
{
    public partial class Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Secretaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Mechanics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Admins",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Secretaries");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Mechanics");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Admins");
        }
    }
}
