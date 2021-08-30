using Microsoft.EntityFrameworkCore.Migrations;

namespace YogaMo.WebAPI.Migrations
{
    public partial class YogaHall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YogaHall",
                columns: table => new
                {
                    YogaHallId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YogaId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YogaHall", x => x.YogaHallId);
                    table.ForeignKey(
                        name: "FK_YogaHall_Yoga_YogaId",
                        column: x => x.YogaId,
                        principalTable: "Yoga",
                        principalColumn: "YogaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YogaHall_YogaId",
                table: "YogaHall",
                column: "YogaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YogaHall");
        }
    }
}
