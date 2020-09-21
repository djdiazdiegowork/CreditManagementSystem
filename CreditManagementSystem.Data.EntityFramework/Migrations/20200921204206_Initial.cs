using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditManagementSystem.Data.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditStatus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Credit",
                columns: table => new
                {
                    ID = table.Column<string>(type: "char(36)", nullable: false),
                    ClientID = table.Column<string>(type: "char(36)", nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    DebtPaid = table.Column<double>(nullable: false),
                    CreationDay = table.Column<DateTime>(nullable: false),
                    ModificationDay = table.Column<DateTime>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    CreditStatusID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Credit_CreditStatus_CreditStatusID",
                        column: x => x.CreditStatusID,
                        principalTable: "CreditStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credit_CreditStatusID",
                table: "Credit",
                column: "CreditStatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credit");

            migrationBuilder.DropTable(
                name: "CreditStatus");
        }
    }
}
