using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeProfileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfiles_User_UserId",
                table: "EmployeeProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProfiles",
                table: "EmployeeProfiles");

            migrationBuilder.RenameTable(
                name: "EmployeeProfiles",
                newName: "EmployeeProfile");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProfiles_UserId",
                table: "EmployeeProfile",
                newName: "IX_EmployeeProfile_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProfile",
                table: "EmployeeProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfile_User_UserId",
                table: "EmployeeProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfile_User_UserId",
                table: "EmployeeProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProfile",
                table: "EmployeeProfile");

            migrationBuilder.RenameTable(
                name: "EmployeeProfile",
                newName: "EmployeeProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProfile_UserId",
                table: "EmployeeProfiles",
                newName: "IX_EmployeeProfiles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProfiles",
                table: "EmployeeProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfiles_User_UserId",
                table: "EmployeeProfiles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
