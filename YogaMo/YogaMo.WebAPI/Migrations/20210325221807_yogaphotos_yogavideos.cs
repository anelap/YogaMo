using Microsoft.EntityFrameworkCore.Migrations;

namespace YogaMo.WebAPI.Migrations
{
    public partial class yogaphotos_yogavideos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YogaPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YogaPhoto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YogaVideo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YoutubeID = table.Column<string>(nullable: true),
                    ThumbnailFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YogaVideo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YogaPhoto");

            migrationBuilder.DropTable(
                name: "YogaVideo");
        }
    }
}
