using Microsoft.EntityFrameworkCore.Migrations;

namespace employee_training_tool.DataMigrations
{
    public partial class EnrollmentsForeignKeyFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AssignedLearningPaths_EnrollmentId",
                table: "Enrollments");

            migrationBuilder.AlterColumn<int>(
                name: "EnrollmentId",
                table: "Enrollments",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_LearningPathId",
                table: "Enrollments",
                column: "LearningPathId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AssignedLearningPaths_LearningPathId",
                table: "Enrollments",
                column: "LearningPathId",
                principalTable: "AssignedLearningPaths",
                principalColumn: "AssignedLearningPathId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AssignedLearningPaths_LearningPathId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_LearningPathId",
                table: "Enrollments");

            migrationBuilder.AlterColumn<int>(
                name: "EnrollmentId",
                table: "Enrollments",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AssignedLearningPaths_EnrollmentId",
                table: "Enrollments",
                column: "EnrollmentId",
                principalTable: "AssignedLearningPaths",
                principalColumn: "AssignedLearningPathId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
