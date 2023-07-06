using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCStudents.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStudentsIdCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grading_Students_StudentsId",
                table: "Grading");

            migrationBuilder.DropIndex(
                name: "IX_Grading_StudentsId",
                table: "Grading");

            migrationBuilder.DropColumn(
                name: "StudentsId",
                table: "Grading");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentsId",
                table: "Grading",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grading_StudentsId",
                table: "Grading",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grading_Students_StudentsId",
                table: "Grading",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
