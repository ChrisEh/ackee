using Microsoft.EntityFrameworkCore.Migrations;

namespace Ackee.Data.Migrations
{
    public partial class enhancedMilestones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Milestones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Milestones");
        }
    }
}
