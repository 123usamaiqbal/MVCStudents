using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCStudents.Migrations
{
    /// <inheritdoc />
    public partial class AddNewGrading : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grading",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradingId = table.Column<int>(type: "int", nullable: false),
                    Computer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Physics = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Science = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grading", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grading_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grading_StudentsId",
                table: "Grading",
                column: "StudentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grading");
        }
    }
}
