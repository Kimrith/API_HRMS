using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "EmployeeProfile",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "EmployeeProfile");
        }
    }
}
