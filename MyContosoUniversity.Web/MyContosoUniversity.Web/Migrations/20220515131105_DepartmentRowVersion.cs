using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyContosoUniversity.Web.Migrations
{
    public partial class DepartmentRowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyToken",
                table: "Departments",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyToken",
                table: "Departments");
        }
    }
}
