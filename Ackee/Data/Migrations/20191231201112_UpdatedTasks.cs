using Microsoft.EntityFrameworkCore.Migrations;

namespace Ackee.Data.Migrations
{
    public partial class UpdatedTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilestoneTask_Milestones_MilestoneID",
                table: "MilestoneTask");

            migrationBuilder.DropForeignKey(
                name: "FK_MilestoneTask_AspNetTasks_TaskID",
                table: "MilestoneTask");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_AspNetTasks_TaskID",
                table: "UserTask");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_AspNetUsers_UserID",
                table: "UserTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTask",
                table: "UserTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MilestoneTask",
                table: "MilestoneTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetTasks",
                table: "AspNetTasks");

            migrationBuilder.RenameTable(
                name: "UserTask",
                newName: "UserTasks");

            migrationBuilder.RenameTable(
                name: "MilestoneTask",
                newName: "MilestoneTasks");

            migrationBuilder.RenameTable(
                name: "AspNetTasks",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_UserTask_TaskID",
                table: "UserTasks",
                newName: "IX_UserTasks_TaskID");

            migrationBuilder.RenameIndex(
                name: "IX_MilestoneTask_TaskID",
                table: "MilestoneTasks",
                newName: "IX_MilestoneTasks_TaskID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTasks",
                table: "UserTasks",
                columns: new[] { "UserID", "TaskID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MilestoneTasks",
                table: "MilestoneTasks",
                columns: new[] { "MilestoneID", "TaskID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskID");

            migrationBuilder.AddForeignKey(
                name: "FK_MilestoneTasks_Milestones_MilestoneID",
                table: "MilestoneTasks",
                column: "MilestoneID",
                principalTable: "Milestones",
                principalColumn: "MilestoneID");

            migrationBuilder.AddForeignKey(
                name: "FK_MilestoneTasks_Tasks_TaskID",
                table: "MilestoneTasks",
                column: "TaskID",
                principalTable: "Tasks",
                principalColumn: "TaskID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_Tasks_TaskID",
                table: "UserTasks",
                column: "TaskID",
                principalTable: "Tasks",
                principalColumn: "TaskID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_AspNetUsers_UserID",
                table: "UserTasks",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilestoneTasks_Milestones_MilestoneID",
                table: "MilestoneTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_MilestoneTasks_Tasks_TaskID",
                table: "MilestoneTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_Tasks_TaskID",
                table: "UserTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_AspNetUsers_UserID",
                table: "UserTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTasks",
                table: "UserTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MilestoneTasks",
                table: "MilestoneTasks");

            migrationBuilder.RenameTable(
                name: "UserTasks",
                newName: "UserTask");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "AspNetTasks");

            migrationBuilder.RenameTable(
                name: "MilestoneTasks",
                newName: "MilestoneTask");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasks_TaskID",
                table: "UserTask",
                newName: "IX_UserTask_TaskID");

            migrationBuilder.RenameIndex(
                name: "IX_MilestoneTasks_TaskID",
                table: "MilestoneTask",
                newName: "IX_MilestoneTask_TaskID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTask",
                table: "UserTask",
                columns: new[] { "UserID", "TaskID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetTasks",
                table: "AspNetTasks",
                column: "TaskID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MilestoneTask",
                table: "MilestoneTask",
                columns: new[] { "MilestoneID", "TaskID" });

            migrationBuilder.AddForeignKey(
                name: "FK_MilestoneTask_Milestones_MilestoneID",
                table: "MilestoneTask",
                column: "MilestoneID",
                principalTable: "Milestones",
                principalColumn: "MilestoneID");

            migrationBuilder.AddForeignKey(
                name: "FK_MilestoneTask_AspNetTasks_TaskID",
                table: "MilestoneTask",
                column: "TaskID",
                principalTable: "AspNetTasks",
                principalColumn: "TaskID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_AspNetTasks_TaskID",
                table: "UserTask",
                column: "TaskID",
                principalTable: "AspNetTasks",
                principalColumn: "TaskID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_AspNetUsers_UserID",
                table: "UserTask",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
