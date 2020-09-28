using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoWorkshop.Web.Migrations
{
    public partial class UserId_ClientId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_UserId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ClientId",
                table: "Vehicles",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Clients_ClientId",
                table: "Vehicles",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Clients_ClientId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_ClientId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_UserId",
                table: "Vehicles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
