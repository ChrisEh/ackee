using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ackee.Data.Migrations
{
    public partial class AddedTasksAndTheirRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetTasks",
                columns: table => new
                {
                    TaskID = table.Column<string>(nullable: false),
                    TaskName = table.Column<string>(maxLength: 128, nullable: true),
                    TaskDescription = table.Column<string>(maxLength: 256, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetTasks", x => x.TaskID);
                });

            migrationBuilder.CreateTable(
                name: "MilestoneTask",
                columns: table => new
                {
                    MilestoneID = table.Column<string>(nullable: false),
                    TaskID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestoneTask", x => new { x.MilestoneID, x.TaskID });
                    table.ForeignKey(
                        name: "FK_MilestoneTask_Milestones_MilestoneID",
                        column: x => x.MilestoneID,
                        principalTable: "Milestones",
                        principalColumn: "MilestoneID");
                    table.ForeignKey(
                        name: "FK_MilestoneTask_AspNetTasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "AspNetTasks",
                        principalColumn: "TaskID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTask",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    TaskID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTask", x => new { x.UserID, x.TaskID });
                    table.ForeignKey(
                        name: "FK_UserTask_AspNetTasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "AspNetTasks",
                        principalColumn: "TaskID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTask_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneTask_TaskID",
                table: "MilestoneTask",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTask_TaskID",
                table: "UserTask",
                column: "TaskID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MilestoneTask");

            migrationBuilder.DropTable(
                name: "UserTask");

            migrationBuilder.DropTable(
                name: "AspNetTasks");
        }
    }
}
