using Microsoft.EntityFrameworkCore.Migrations;

namespace employee_training_tool.Data.Migrations
{
    public partial class UpdateIdentitySchema2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);
        }
    }
}
