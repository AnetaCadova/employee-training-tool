using Microsoft.EntityFrameworkCore.Migrations;

namespace employee_training_tool.DataMigrations
{
    public partial class ProgressBar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AssignedTasks",
                newName: "State");

            migrationBuilder.AddColumn<double>(
                name: "Progress",
                table: "AssignedLearningPaths",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progress",
                table: "AssignedLearningPaths");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "AssignedTasks",
                newName: "Status");
        }
    }
}
