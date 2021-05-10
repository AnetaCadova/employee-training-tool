using Microsoft.EntityFrameworkCore.Migrations;

namespace employee_training_tool.DataMigrations
{
    public partial class AuthorsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "LearningPaths",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "CatalogTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LearningPaths_AuthorId",
                table: "LearningPaths",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogTasks_AuthorId",
                table: "CatalogTasks",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogTasks_AspNetUsers_AuthorId",
                table: "CatalogTasks",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LearningPaths_AspNetUsers_AuthorId",
                table: "LearningPaths",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogTasks_AspNetUsers_AuthorId",
                table: "CatalogTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_LearningPaths_AspNetUsers_AuthorId",
                table: "LearningPaths");

            migrationBuilder.DropIndex(
                name: "IX_LearningPaths_AuthorId",
                table: "LearningPaths");

            migrationBuilder.DropIndex(
                name: "IX_CatalogTasks_AuthorId",
                table: "CatalogTasks");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "LearningPaths");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "CatalogTasks");
        }
    }
}
