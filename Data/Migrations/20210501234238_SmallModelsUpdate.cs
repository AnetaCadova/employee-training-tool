using Microsoft.EntityFrameworkCore.Migrations;

namespace employee_training_tool.DataMigrations
{
    public partial class SmallModelsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentId",
                table: "AssignedLearningPaths",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssignedLearningPaths_MentorId",
                table: "AssignedLearningPaths",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedLearningPaths_NewComerId",
                table: "AssignedLearningPaths",
                column: "NewComerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedLearningPaths_AspNetUsers_MentorId",
                table: "AssignedLearningPaths",
                column: "MentorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedLearningPaths_AspNetUsers_NewComerId",
                table: "AssignedLearningPaths",
                column: "NewComerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AssignedLearningPaths_EnrollmentId",
                table: "Enrollments",
                column: "EnrollmentId",
                principalTable: "AssignedLearningPaths",
                principalColumn: "AssignedLearningPathId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedLearningPaths_AspNetUsers_MentorId",
                table: "AssignedLearningPaths");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedLearningPaths_AspNetUsers_NewComerId",
                table: "AssignedLearningPaths");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AssignedLearningPaths_EnrollmentId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_AssignedLearningPaths_MentorId",
                table: "AssignedLearningPaths");

            migrationBuilder.DropIndex(
                name: "IX_AssignedLearningPaths_NewComerId",
                table: "AssignedLearningPaths");

            migrationBuilder.DropColumn(
                name: "EnrollmentId",
                table: "AssignedLearningPaths");

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
                column: "LearningPathId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AssignedLearningPaths_LearningPathId",
                table: "Enrollments",
                column: "LearningPathId",
                principalTable: "AssignedLearningPaths",
                principalColumn: "AssignedLearningPathId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
