using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFoodApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFoodItem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "FoodItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "FoodItems");
        }
    }
}
