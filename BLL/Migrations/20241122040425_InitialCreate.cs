using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Students_StudentId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Books",
                type: "int",
                maxLength: 12,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Students_StudentId",
                table: "Books",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Students_StudentId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 12);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Students_StudentId",
                table: "Books",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
