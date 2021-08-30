using Microsoft.EntityFrameworkCore.Migrations;

namespace YogaMo.WebAPI.Migrations
{
    public partial class AddLastSeen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastSeenClient",
                table: "ChatInstructorsClients",
                nullable: true,
                defaultValue: "0");

            migrationBuilder.AddColumn<string>(
                name: "LastSeenInstructor",
                table: "ChatInstructorsClients",
                nullable: true,
                defaultValue: "0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSeenClient",
                table: "ChatInstructorsClients");

            migrationBuilder.DropColumn(
                name: "LastSeenInstructor",
                table: "ChatInstructorsClients");
        }
    }
}
