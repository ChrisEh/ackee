using Microsoft.EntityFrameworkCore.Migrations;

namespace Ackee.Data.Migrations
{
    public partial class UpdatedMilestoneProjectRelationToBeSimilarToProjectOwnerRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Projects_ProjectId",
                table: "Milestones");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Milestones",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Milestones_ProjectId",
                table: "Milestones",
                newName: "IX_Milestones_ProjectID");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectID",
                table: "Milestones",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Projects_ProjectID",
                table: "Milestones",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Projects_ProjectID",
                table: "Milestones");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "Milestones",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Milestones_ProjectID",
                table: "Milestones",
                newName: "IX_Milestones_ProjectId");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Milestones",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Projects_ProjectId",
                table: "Milestones",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
