using Microsoft.EntityFrameworkCore.Migrations;

namespace employee_training_tool.DataMigrations
{
    public partial class TaskModelsChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "LearningPaths",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LearningPaths",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CatalogTasks",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CatalogTasks",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearningPathTasks_CatalogTaskId",
                table: "LearningPathTasks",
                column: "CatalogTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_CatalogTaskId",
                table: "AssignedTasks",
                column: "CatalogTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTasks_CatalogTasks_CatalogTaskId",
                table: "AssignedTasks",
                column: "CatalogTaskId",
                principalTable: "CatalogTasks",
                principalColumn: "CatalogTaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LearningPathTasks_CatalogTasks_CatalogTaskId",
                table: "LearningPathTasks",
                column: "CatalogTaskId",
                principalTable: "CatalogTasks",
                principalColumn: "CatalogTaskId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_CatalogTasks_CatalogTaskId",
                table: "AssignedTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_LearningPathTasks_CatalogTasks_CatalogTaskId",
                table: "LearningPathTasks");

            migrationBuilder.DropIndex(
                name: "IX_LearningPathTasks_CatalogTaskId",
                table: "LearningPathTasks");

            migrationBuilder.DropIndex(
                name: "IX_AssignedTasks_CatalogTaskId",
                table: "AssignedTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "LearningPaths",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LearningPaths",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CatalogTasks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CatalogTasks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
