using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoWorkshop.Web.Migrations
{
    public partial class Mecanic_Typo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Mecanics_MecanicId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "Mecanics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialty",
                table: "Specialty");

            migrationBuilder.RenameTable(
                name: "Specialty",
                newName: "Specialties");

            migrationBuilder.RenameColumn(
                name: "MecanicId",
                table: "Appointments",
                newName: "MechanicId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_MecanicId",
                table: "Appointments",
                newName: "IX_Appointments_MechanicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Mechanics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: false),
                    TaxIdentificationNumber = table.Column<string>(nullable: true),
                    CitizenCardNumber = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    SpecialtyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mechanics_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mechanics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mechanics_SpecialtyId",
                table: "Mechanics",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Mechanics_UserId",
                table: "Mechanics",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Mechanics_MechanicId",
                table: "Appointments",
                column: "MechanicId",
                principalTable: "Mechanics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Mechanics_MechanicId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "Mechanics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties");

            migrationBuilder.RenameTable(
                name: "Specialties",
                newName: "Specialty");

            migrationBuilder.RenameColumn(
                name: "MechanicId",
                table: "Appointments",
                newName: "MecanicId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_MechanicId",
                table: "Appointments",
                newName: "IX_Appointments_MecanicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialty",
                table: "Specialty",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Mecanics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CitizenCardNumber = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    SpecialtyId = table.Column<int>(nullable: false),
                    StreetAddress = table.Column<string>(nullable: true),
                    TaxIdentificationNumber = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mecanics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mecanics_Specialty_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mecanics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mecanics_SpecialtyId",
                table: "Mecanics",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Mecanics_UserId",
                table: "Mecanics",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Mecanics_MecanicId",
                table: "Appointments",
                column: "MecanicId",
                principalTable: "Mecanics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
