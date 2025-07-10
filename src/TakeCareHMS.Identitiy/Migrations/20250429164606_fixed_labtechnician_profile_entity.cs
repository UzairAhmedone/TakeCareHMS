using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeCareHMS.Identitiy.Migrations
{
    /// <inheritdoc />
    public partial class fixed_labtechnician_profile_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NurseProfile_UserId",
                table: "NurseProfile");

            migrationBuilder.RenameColumn(
                name: "LicenseNo",
                table: "LabTechnicianProfile",
                newName: "Certification");

            migrationBuilder.CreateIndex(
                name: "IX_NurseProfile_UserId",
                table: "NurseProfile",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NurseProfile_UserId",
                table: "NurseProfile");

            migrationBuilder.RenameColumn(
                name: "Certification",
                table: "LabTechnicianProfile",
                newName: "LicenseNo");

            migrationBuilder.CreateIndex(
                name: "IX_NurseProfile_UserId",
                table: "NurseProfile",
                column: "UserId");
        }
    }
}
