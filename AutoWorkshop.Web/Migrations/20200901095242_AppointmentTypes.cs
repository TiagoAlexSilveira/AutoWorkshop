using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoWorkshop.Web.Migrations
{
    public partial class AppointmentTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentType_AppointmentTypeId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentType",
                table: "AppointmentType");

            migrationBuilder.RenameTable(
                name: "AppointmentType",
                newName: "AppointmentTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentTypes",
                table: "AppointmentTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentTypes_AppointmentTypeId",
                table: "Appointments",
                column: "AppointmentTypeId",
                principalTable: "AppointmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentTypes_AppointmentTypeId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentTypes",
                table: "AppointmentTypes");

            migrationBuilder.RenameTable(
                name: "AppointmentTypes",
                newName: "AppointmentType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentType",
                table: "AppointmentType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentType_AppointmentTypeId",
                table: "Appointments",
                column: "AppointmentTypeId",
                principalTable: "AppointmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
