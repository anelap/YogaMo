using Microsoft.EntityFrameworkCore.Migrations;

namespace YogaMo.WebAPI.Migrations
{
    public partial class ProductDetailsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "OrderItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "OrderItem");
        }
    }
}
