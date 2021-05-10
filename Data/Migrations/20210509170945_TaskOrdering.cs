using Microsoft.EntityFrameworkCore.Migrations;

namespace employee_training_tool.DataMigrations
{
    public partial class TaskOrdering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "LearningPathTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "AssignedTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "LearningPathTasks");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "AssignedTasks");
        }
    }
}
