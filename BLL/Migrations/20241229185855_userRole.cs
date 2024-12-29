using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLL.Migrations
{
    public partial class UserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert roles into the Roles table
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove seeded roles
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2 }
            );
        }
    }
}