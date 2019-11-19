using Microsoft.EntityFrameworkCore.Migrations;

namespace Ackee.Data.Migrations
{
    public partial class AspNetMilestones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Project_ProjectID1",
                table: "Milestones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Milestones",
                table: "Milestones");

            migrationBuilder.DropIndex(
                name: "IX_Milestones_ProjectID1",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "ProjectID1",
                table: "Milestones");

            migrationBuilder.AddColumn<string>(
                name: "MilestoneID",
                table: "Milestones",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Milestones",
                table: "Milestones",
                column: "MilestoneID");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_ProjectID",
                table: "Milestones",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Project_ProjectID",
                table: "Milestones",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Project_ProjectID",
                table: "Milestones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Milestones",
                table: "Milestones");

            migrationBuilder.DropIndex(
                name: "IX_Milestones_ProjectID",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "MilestoneID",
                table: "Milestones");

            migrationBuilder.AddColumn<string>(
                name: "ProjectID1",
                table: "Milestones",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Milestones",
                table: "Milestones",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_ProjectID1",
                table: "Milestones",
                column: "ProjectID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Project_ProjectID1",
                table: "Milestones",
                column: "ProjectID1",
                principalTable: "Project",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
