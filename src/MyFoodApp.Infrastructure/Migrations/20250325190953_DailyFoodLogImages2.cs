using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFoodApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DailyFoodLogImages2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipePhoto_Recipes_RecipeId",
                table: "RecipePhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipePhoto",
                table: "RecipePhoto");

            migrationBuilder.RenameTable(
                name: "RecipePhoto",
                newName: "RecipePhotos");

            migrationBuilder.RenameIndex(
                name: "IX_RecipePhoto_RecipeId",
                table: "RecipePhotos",
                newName: "IX_RecipePhotos_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipePhotos",
                table: "RecipePhotos",
                column: "RecipePhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipePhotos_Recipes_RecipeId",
                table: "RecipePhotos",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipePhotos_Recipes_RecipeId",
                table: "RecipePhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipePhotos",
                table: "RecipePhotos");

            migrationBuilder.RenameTable(
                name: "RecipePhotos",
                newName: "RecipePhoto");

            migrationBuilder.RenameIndex(
                name: "IX_RecipePhotos_RecipeId",
                table: "RecipePhoto",
                newName: "IX_RecipePhoto_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipePhoto",
                table: "RecipePhoto",
                column: "RecipePhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipePhoto_Recipes_RecipeId",
                table: "RecipePhoto",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
