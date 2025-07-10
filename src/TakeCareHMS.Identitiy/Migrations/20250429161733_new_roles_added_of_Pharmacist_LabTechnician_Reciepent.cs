using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeCareHMS.Identitiy.Migrations
{
    /// <inheritdoc />
    public partial class new_roles_added_of_Pharmacist_LabTechnician_Reciepent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabTechnicianProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenseNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTechnicianProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabTechnicianProfile_HmsUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "HmsUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NurseProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LicenseNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NurseProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NurseProfile_HmsUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "HmsUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PharmacistProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenseNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacistProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PharmacistProfile_HmsUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "HmsUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabTechnicianProfile_UserId",
                table: "LabTechnicianProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NurseProfile_UserId",
                table: "NurseProfile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacistProfile_UserId",
                table: "PharmacistProfile",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabTechnicianProfile");

            migrationBuilder.DropTable(
                name: "NurseProfile");

            migrationBuilder.DropTable(
                name: "PharmacistProfile");
        }
    }
}
