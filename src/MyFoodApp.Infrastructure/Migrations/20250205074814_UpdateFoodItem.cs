using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFoodApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFoodItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CarbohydratesPerUnit",
                table: "FoodItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FatPerUnit",
                table: "FoodItems",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbohydratesPerUnit",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "FatPerUnit",
                table: "FoodItems");
        }
    }
}
