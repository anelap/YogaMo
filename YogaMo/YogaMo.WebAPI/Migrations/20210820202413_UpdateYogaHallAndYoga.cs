using Microsoft.EntityFrameworkCore.Migrations;

namespace YogaMo.WebAPI.Migrations
{
    public partial class UpdateYogaHallAndYoga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YogaHall_Yoga_YogaId",
                table: "YogaHall");

            migrationBuilder.DropIndex(
                name: "IX_YogaHall_YogaId",
                table: "YogaHall");

            migrationBuilder.DropColumn(
                name: "YogaId",
                table: "YogaHall");

            migrationBuilder.AddColumn<int>(
                name: "YogaHallId",
                table: "YogaClass",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_YogaClass_YogaHallId",
                table: "YogaClass",
                column: "YogaHallId");

            migrationBuilder.AddForeignKey(
                name: "FK_YogaClass_YogaHall_YogaHallId",
                table: "YogaClass",
                column: "YogaHallId",
                principalTable: "YogaHall",
                principalColumn: "YogaHallId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YogaClass_YogaHall_YogaHallId",
                table: "YogaClass");

            migrationBuilder.DropIndex(
                name: "IX_YogaClass_YogaHallId",
                table: "YogaClass");

            migrationBuilder.DropColumn(
                name: "YogaHallId",
                table: "YogaClass");

            migrationBuilder.AddColumn<int>(
                name: "YogaId",
                table: "YogaHall",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_YogaHall_YogaId",
                table: "YogaHall",
                column: "YogaId");

            migrationBuilder.AddForeignKey(
                name: "FK_YogaHall_Yoga_YogaId",
                table: "YogaHall",
                column: "YogaId",
                principalTable: "Yoga",
                principalColumn: "YogaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
