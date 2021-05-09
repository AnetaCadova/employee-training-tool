using Microsoft.EntityFrameworkCore.Migrations;

namespace employee_training_tool.DataMigrations
{
    public partial class IdentityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_MentorId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_MentorId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_NewComerId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_LearningPaths_LearningPathID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_LearningPaths_AssignedLearningPathLearningPathID",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_LearningPaths_LearningPathID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MentorId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssignedLearningPathLearningPathID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "LearningPaths");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MentorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "AssignedLearningPathLearningPathID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "AssignedTasks");

            migrationBuilder.RenameColumn(
                name: "LearningPathID",
                table: "LearningPaths",
                newName: "LearningPathId");

            migrationBuilder.RenameColumn(
                name: "LearningPathID",
                table: "Enrollments",
                newName: "LearningPathId");

            migrationBuilder.RenameColumn(
                name: "EnrollmentID",
                table: "Enrollments",
                newName: "EnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_LearningPathID",
                table: "Enrollments",
                newName: "IX_Enrollments_LearningPathId");

            migrationBuilder.RenameColumn(
                name: "LearningPathID",
                table: "AssignedTasks",
                newName: "LearningPathId");

            migrationBuilder.RenameColumn(
                name: "TaskTypes",
                table: "AssignedTasks",
                newName: "AssignedLearningPathId");

            migrationBuilder.RenameColumn(
                name: "TaskID",
                table: "AssignedTasks",
                newName: "TaskType");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_LearningPathID",
                table: "AssignedTasks",
                newName: "IX_AssignedTasks_LearningPathId");

            migrationBuilder.AlterColumn<int>(
                name: "NewComerId",
                table: "Enrollments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MentorId",
                table: "Enrollments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LearningPathId",
                table: "Enrollments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetRoles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "AssignedTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaskType",
                table: "AssignedTasks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedTaskId",
                table: "AssignedTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "CatalogTaskId",
                table: "AssignedTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignedTasks",
                table: "AssignedTasks",
                column: "AssignedTaskId");

            migrationBuilder.CreateTable(
                name: "AssignedLearningPaths",
                columns: table => new
                {
                    AssignedLearningPathId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OriginalLearningPathId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedLearningPaths", x => x.AssignedLearningPathId);
                    table.ForeignKey(
                        name: "FK_AssignedLearningPaths_LearningPaths_OriginalLearningPathId",
                        column: x => x.OriginalLearningPathId,
                        principalTable: "LearningPaths",
                        principalColumn: "LearningPathId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogTasks",
                columns: table => new
                {
                    CatalogTaskId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    TaskType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogTasks", x => x.CatalogTaskId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_AssignedLearningPathId",
                table: "AssignedTasks",
                column: "AssignedLearningPathId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedLearningPaths_OriginalLearningPathId",
                table: "AssignedLearningPaths",
                column: "OriginalLearningPathId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_MentorId",
                table: "Enrollments",
                column: "MentorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_NewComerId",
                table: "Enrollments",
                column: "NewComerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_AssignedTasks_AssignedLearningPaths_AssignedLearningPathId",
                table: "AssignedTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTasks_LearningPaths_LearningPathId",
                table: "AssignedTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_MentorId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_NewComerId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AssignedLearningPaths_LearningPathId",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "AssignedLearningPaths");

            migrationBuilder.DropTable(
                name: "CatalogTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignedTasks",
                table: "AssignedTasks");

            migrationBuilder.DropIndex(
                name: "IX_AssignedTasks_AssignedLearningPathId",
                table: "AssignedTasks");

            migrationBuilder.DropColumn(
                name: "AssignedTaskId",
                table: "AssignedTasks");

            migrationBuilder.DropColumn(
                name: "CatalogTaskId",
                table: "AssignedTasks");

            migrationBuilder.RenameTable(
                name: "AssignedTasks",
                newName: "Tasks");

            migrationBuilder.RenameColumn(
                name: "LearningPathId",
                table: "LearningPaths",
                newName: "LearningPathID");

            migrationBuilder.RenameColumn(
                name: "LearningPathId",
                table: "Enrollments",
                newName: "LearningPathID");

            migrationBuilder.RenameColumn(
                name: "EnrollmentId",
                table: "Enrollments",
                newName: "EnrollmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_LearningPathId",
                table: "Enrollments",
                newName: "IX_Enrollments_LearningPathID");

            migrationBuilder.RenameColumn(
                name: "LearningPathId",
                table: "Tasks",
                newName: "LearningPathID");

            migrationBuilder.RenameColumn(
                name: "TaskType",
                table: "Tasks",
                newName: "TaskID");

            migrationBuilder.RenameColumn(
                name: "AssignedLearningPathId",
                table: "Tasks",
                newName: "TaskTypes");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedTasks_LearningPathId",
                table: "Tasks",
                newName: "IX_Tasks_LearningPathID");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "LearningPaths",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NewComerId",
                table: "Enrollments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "MentorId",
                table: "Enrollments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "LearningPathID",
                table: "Enrollments",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MentorId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetRoles",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Tasks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "TaskID",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedLearningPathLearningPathID",
                table: "Tasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MentorId",
                table: "AspNetUsers",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedLearningPathLearningPathID",
                table: "Tasks",
                column: "AssignedLearningPathLearningPathID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_MentorId",
                table: "AspNetUsers",
                column: "MentorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_MentorId",
                table: "Enrollments",
                column: "MentorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_NewComerId",
                table: "Enrollments",
                column: "NewComerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_LearningPaths_LearningPathID",
                table: "Enrollments",
                column: "LearningPathID",
                principalTable: "LearningPaths",
                principalColumn: "LearningPathID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_LearningPaths_AssignedLearningPathLearningPathID",
                table: "Tasks",
                column: "AssignedLearningPathLearningPathID",
                principalTable: "LearningPaths",
                principalColumn: "LearningPathID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_LearningPaths_LearningPathID",
                table: "Tasks",
                column: "LearningPathID",
                principalTable: "LearningPaths",
                principalColumn: "LearningPathID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
