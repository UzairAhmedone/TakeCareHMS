using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeCareHMS.Identitiy.Migrations
{
    /// <inheritdoc />
    public partial class added_doctor_profile_entity_and_modified_Hms_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HmsUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    City = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PlotNo = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    FlatNo = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HmsUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HmsUsers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LicenseNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorProfile_HmsUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "HmsUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfile_UserId",
                table: "DoctorProfile",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorProfile");

            migrationBuilder.DropTable(
                name: "HmsUsers");
        }
    }
}
