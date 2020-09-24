using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoWorkshop.Web.Migrations
{
    public partial class AppointmentChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Appointments",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Appointments",
                newName: "EndTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Appointments",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Appointments",
                newName: "Date");
        }
    }
}
