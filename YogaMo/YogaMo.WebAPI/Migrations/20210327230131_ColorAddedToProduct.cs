using Microsoft.EntityFrameworkCore.Migrations;

namespace YogaMo.WebAPI.Migrations
{
    public partial class ColorAddedToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Product");
        }
    }
}
