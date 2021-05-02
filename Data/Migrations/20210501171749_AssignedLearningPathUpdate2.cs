using Microsoft.EntityFrameworkCore.Migrations;

namespace employee_training_tool.DataMigrations
{
    public partial class AssignedLearningPathUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_AssignedLearningPaths_AssignedLearningPathId",
                table: "AssignedTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_LearningPaths_LearningPathId",
                table: "AssignedTasks");

            migrationBuilder.RenameColumn(
                name: "LearningPathId",
                table: "AssignedTasks",
                newName: "OriginalLearningPathId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedTasks_LearningPathId",
                table: "AssignedTasks",
                newName: "IX_AssignedTasks_OriginalLearningPathId");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedLearningPathId",
                table: "AssignedTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_AssignedLearningPaths_AssignedLearningPathId",
                table: "AssignedTasks",
                column: "AssignedLearningPathId",
                principalTable: "AssignedLearningPaths",
                principalColumn: "AssignedLearningPathId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_LearningPaths_OriginalLearningPathId",
                table: "AssignedTasks",
                column: "OriginalLearningPathId",
                principalTable: "LearningPaths",
                principalColumn: "LearningPathId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_AssignedLearningPaths_AssignedLearningPathId",
                table: "AssignedTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_LearningPaths_OriginalLearningPathId",
                table: "AssignedTasks");

            migrationBuilder.RenameColumn(
                name: "OriginalLearningPathId",
                table: "AssignedTasks",
                newName: "LearningPathId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedTasks_OriginalLearningPathId",
                table: "AssignedTasks",
                newName: "IX_AssignedTasks_LearningPathId");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedLearningPathId",
                table: "AssignedTasks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_AssignedLearningPaths_AssignedLearningPathId",
                table: "AssignedTasks",
                column: "AssignedLearningPathId",
                principalTable: "AssignedLearningPaths",
                principalColumn: "AssignedLearningPathId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_LearningPaths_LearningPathId",
                table: "AssignedTasks",
                column: "LearningPathId",
                principalTable: "LearningPaths",
                principalColumn: "LearningPathId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
