using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPaylipDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Payslips",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "Allowances",
                table: "Payslips",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BasicSalary",
                table: "Payslips",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OvertimePay",
                table: "Payslips",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "PayDate",
                table: "Payslips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Payslips_EmployeeId",
                table: "Payslips",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payslips_EmployeeProfile_EmployeeId",
                table: "Payslips",
                column: "EmployeeId",
                principalTable: "EmployeeProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payslips_EmployeeProfile_EmployeeId",
                table: "Payslips");

            migrationBuilder.DropIndex(
                name: "IX_Payslips_EmployeeId",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "Allowances",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "BasicSalary",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "OvertimePay",
                table: "Payslips");

            migrationBuilder.DropColumn(
                name: "PayDate",
                table: "Payslips");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Payslips",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
