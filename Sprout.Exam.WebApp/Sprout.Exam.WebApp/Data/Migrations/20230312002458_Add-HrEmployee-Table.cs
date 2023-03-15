using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprout.Exam.WebApp.Data.Migrations
{
    public partial class AddHrEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HrEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TIN = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmployeeType = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrEmployees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HrEmployees_Name_BirthDate_TIN_EmployeeType_IsDeleted",
                table: "HrEmployees",
                columns: new[] { "Name", "BirthDate", "TIN", "EmployeeType", "IsDeleted" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HrEmployees");
        }
    }
}
