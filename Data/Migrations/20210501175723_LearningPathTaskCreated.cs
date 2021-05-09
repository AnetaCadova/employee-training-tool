using Microsoft.EntityFrameworkCore.Migrations;

namespace employee_training_tool.DataMigrations
{
    public partial class LearningPathTaskCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_LearningPaths_OriginalLearningPathId",
                table: "AssignedTasks");

            migrationBuilder.DropIndex(
                name: "IX_AssignedTasks_OriginalLearningPathId",
                table: "AssignedTasks");

            migrationBuilder.DropColumn(
                name: "OriginalLearningPathId",
                table: "AssignedTasks");

            migrationBuilder.CreateTable(
                name: "LearningPathTasks",
                columns: table => new
                {
                    LearningPathTaskId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LearningPathId = table.Column<int>(type: "INTEGER", nullable: false),
                    CatalogTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    TaskType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPathTasks", x => x.LearningPathTaskId);
                    table.ForeignKey(
                        name: "FK_LearningPathTasks_LearningPaths_LearningPathId",
                        column: x => x.LearningPathId,
                        principalTable: "LearningPaths",
                        principalColumn: "LearningPathId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearningPathTasks_LearningPathId",
                table: "LearningPathTasks",
                column: "LearningPathId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearningPathTasks");

            migrationBuilder.AddColumn<int>(
                name: "OriginalLearningPathId",
                table: "AssignedTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_OriginalLearningPathId",
                table: "AssignedTasks",
                column: "OriginalLearningPathId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_LearningPaths_OriginalLearningPathId",
                table: "AssignedTasks",
                column: "OriginalLearningPathId",
                principalTable: "LearningPaths",
                principalColumn: "LearningPathId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
