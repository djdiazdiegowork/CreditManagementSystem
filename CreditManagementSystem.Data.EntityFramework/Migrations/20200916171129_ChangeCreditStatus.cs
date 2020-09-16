using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditManagementSystem.Data.EntityFramework.Migrations
{
    public partial class ChangeCreditStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DebtPaid",
                table: "Credit",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDay",
                table: "Credit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModificationDay",
                table: "Credit");

            migrationBuilder.AlterColumn<double>(
                name: "DebtPaid",
                table: "Credit",
                type: "double",
                nullable: true,
                oldClrType: typeof(double));
        }
    }
}
