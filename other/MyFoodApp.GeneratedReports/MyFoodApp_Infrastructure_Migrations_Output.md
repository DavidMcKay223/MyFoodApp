# Directory: Migrations

## File: 20250202045404_InitialMigration.cs

```C#
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFoodApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodCategories",
                columns: table => new
                {
                    FoodCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCategories", x => x.FoodCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "MealSuggestions",
                columns: table => new
                {
                    MealSuggestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSuggestions", x => x.MealSuggestionId);
                });

            migrationBuilder.CreateTable(
                name: "MealSuggestionTags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSuggestionTags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PrepTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    CookTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    Servings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "StoreSections",
                columns: table => new
                {
                    StoreSectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreSections", x => x.StoreSectionId);
                });

            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    FoodItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FoodCategoryId = table.Column<int>(type: "int", nullable: false),
                    CaloriesPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProteinPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.FoodItemId);
                    table.ForeignKey(
                        name: "FK_FoodItems_FoodCategories_FoodCategoryId",
                        column: x => x.FoodCategoryId,
                        principalTable: "FoodCategories",
                        principalColumn: "FoodCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealSuggestionTagMapping",
                columns: table => new
                {
                    MealSuggestionId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSuggestionTagMapping", x => new { x.MealSuggestionId, x.TagId });
                    table.ForeignKey(
                        name: "FK_MealSuggestionTagMapping_MealSuggestionTags_TagId",
                        column: x => x.TagId,
                        principalTable: "MealSuggestionTags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealSuggestionTagMapping_MealSuggestions_MealSuggestionId",
                        column: x => x.MealSuggestionId,
                        principalTable: "MealSuggestions",
                        principalColumn: "MealSuggestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeMealSuggestions",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    MealSuggestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeMealSuggestions", x => new { x.RecipeId, x.MealSuggestionId });
                    table.ForeignKey(
                        name: "FK_RecipeMealSuggestions_MealSuggestions_MealSuggestionId",
                        column: x => x.MealSuggestionId,
                        principalTable: "MealSuggestions",
                        principalColumn: "MealSuggestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeMealSuggestions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeSteps",
                columns: table => new
                {
                    StepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    Instruction = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeSteps", x => x.StepId);
                    table.ForeignKey(
                        name: "FK_RecipeSteps_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodItemStoreSections",
                columns: table => new
                {
                    FoodItemId = table.Column<int>(type: "int", nullable: false),
                    StoreSectionId = table.Column<int>(type: "int", nullable: false),
                    ShelfNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItemStoreSections", x => new { x.FoodItemId, x.StoreSectionId });
                    table.ForeignKey(
                        name: "FK_FoodItemStoreSections_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItemStoreSections_StoreSections_StoreSectionId",
                        column: x => x.StoreSectionId,
                        principalTable: "StoreSections",
                        principalColumn: "StoreSectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    FoodItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredients_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceHistories",
                columns: table => new
                {
                    PriceHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodItemId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceHistories", x => x.PriceHistoryId);
                    table.ForeignKey(
                        name: "FK_PriceHistories_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_FoodCategoryId",
                table: "FoodItems",
                column: "FoodCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItemStoreSections_StoreSectionId",
                table: "FoodItemStoreSections",
                column: "StoreSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_FoodItemId",
                table: "Ingredients",
                column: "FoodItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_MealSuggestionTagMapping_TagId",
                table: "MealSuggestionTagMapping",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceHistories_FoodItemId",
                table: "PriceHistories",
                column: "FoodItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMealSuggestions_MealSuggestionId",
                table: "RecipeMealSuggestions",
                column: "MealSuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSteps_RecipeId",
                table: "RecipeSteps",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItemStoreSections");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "MealSuggestionTagMapping");

            migrationBuilder.DropTable(
                name: "PriceHistories");

            migrationBuilder.DropTable(
                name: "RecipeMealSuggestions");

            migrationBuilder.DropTable(
                name: "RecipeSteps");

            migrationBuilder.DropTable(
                name: "StoreSections");

            migrationBuilder.DropTable(
                name: "MealSuggestionTags");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "MealSuggestions");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "FoodCategories");
        }
    }
}

```

## File: 20250202045404_InitialMigration.Designer.cs

```C#
// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyFoodApp.Infrastructure.Persistence;

#nullable disable

namespace MyFoodApp.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250202045404_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MealSuggestionTagMapping", b =>
                {
                    b.Property<int>("MealSuggestionId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("MealSuggestionId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("MealSuggestionTagMapping");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodCategory", b =>
                {
                    b.Property<int>("FoodCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodCategoryId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("FoodCategoryId");

                    b.ToTable("FoodCategories");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.Property<int>("FoodItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodItemId"));

                    b.Property<decimal?>("CaloriesPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("FoodCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal?>("ProteinPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("FoodItemId");

                    b.HasIndex("FoodCategoryId");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItemStoreSection", b =>
                {
                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<int>("StoreSectionId")
                        .HasColumnType("int");

                    b.Property<int?>("ShelfNumber")
                        .HasColumnType("int");

                    b.HasKey("FoodItemId", "StoreSectionId");

                    b.HasIndex("StoreSectionId");

                    b.ToTable("FoodItemStoreSections");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("IngredientId");

                    b.HasIndex("FoodItemId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestion", b =>
                {
                    b.Property<int>("MealSuggestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealSuggestionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MealType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("MealSuggestionId");

                    b.ToTable("MealSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestionTag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TagId");

                    b.ToTable("MealSuggestionTags");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.PriceHistory", b =>
                {
                    b.Property<int>("PriceHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriceHistoryId"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PriceHistoryId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("PriceHistories");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<int>("CookTimeMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("PrepTimeMinutes")
                        .HasColumnType("int");

                    b.Property<int>("Servings")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeMealSuggestion", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("MealSuggestionId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "MealSuggestionId");

                    b.HasIndex("MealSuggestionId");

                    b.ToTable("RecipeMealSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeStep", b =>
                {
                    b.Property<int>("StepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StepId"));

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("StepNumber")
                        .HasColumnType("int");

                    b.HasKey("StepId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeSteps");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.StoreSection", b =>
                {
                    b.Property<int>("StoreSectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreSectionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StoreSectionId");

                    b.ToTable("StoreSections");
                });

            modelBuilder.Entity("MealSuggestionTagMapping", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestion", null)
                        .WithMany()
                        .HasForeignKey("MealSuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestionTag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodCategory", "FoodCategory")
                        .WithMany("FoodItems")
                        .HasForeignKey("FoodCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodCategory");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItemStoreSection", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("StoreSections")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.StoreSection", "StoreSection")
                        .WithMany("FoodItems")
                        .HasForeignKey("StoreSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("StoreSection");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Ingredient", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("Ingredients")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.PriceHistory", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("PriceHistories")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeMealSuggestion", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestion", "MealSuggestion")
                        .WithMany("RecipeSuggestions")
                        .HasForeignKey("MealSuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("MealSuggestions")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealSuggestion");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeStep", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodCategory", b =>
                {
                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("PriceHistories");

                    b.Navigation("StoreSections");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestion", b =>
                {
                    b.Navigation("RecipeSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("MealSuggestions");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.StoreSection", b =>
                {
                    b.Navigation("FoodItems");
                });
#pragma warning restore 612, 618
        }
    }
}

```

## File: 20250205074814_UpdateFoodItem.cs

```C#
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

```

## File: 20250205074814_UpdateFoodItem.Designer.cs

```C#
// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyFoodApp.Infrastructure.Persistence;

#nullable disable

namespace MyFoodApp.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250205074814_UpdateFoodItem")]
    partial class UpdateFoodItem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MealSuggestionTagMapping", b =>
                {
                    b.Property<int>("MealSuggestionId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("MealSuggestionId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("MealSuggestionTagMapping");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodCategory", b =>
                {
                    b.Property<int>("FoodCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodCategoryId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("FoodCategoryId");

                    b.ToTable("FoodCategories");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.Property<int>("FoodItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodItemId"));

                    b.Property<decimal?>("CaloriesPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CarbohydratesPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<decimal?>("FatPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FoodCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal?>("ProteinPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("FoodItemId");

                    b.HasIndex("FoodCategoryId");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItemStoreSection", b =>
                {
                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<int>("StoreSectionId")
                        .HasColumnType("int");

                    b.Property<int?>("ShelfNumber")
                        .HasColumnType("int");

                    b.HasKey("FoodItemId", "StoreSectionId");

                    b.HasIndex("StoreSectionId");

                    b.ToTable("FoodItemStoreSections");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("IngredientId");

                    b.HasIndex("FoodItemId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestion", b =>
                {
                    b.Property<int>("MealSuggestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealSuggestionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MealType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("MealSuggestionId");

                    b.ToTable("MealSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestionTag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TagId");

                    b.ToTable("MealSuggestionTags");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.PriceHistory", b =>
                {
                    b.Property<int>("PriceHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriceHistoryId"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PriceHistoryId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("PriceHistories");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<int>("CookTimeMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("PrepTimeMinutes")
                        .HasColumnType("int");

                    b.Property<int>("Servings")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeMealSuggestion", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("MealSuggestionId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "MealSuggestionId");

                    b.HasIndex("MealSuggestionId");

                    b.ToTable("RecipeMealSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeStep", b =>
                {
                    b.Property<int>("StepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StepId"));

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("StepNumber")
                        .HasColumnType("int");

                    b.HasKey("StepId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeSteps");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.StoreSection", b =>
                {
                    b.Property<int>("StoreSectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreSectionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StoreSectionId");

                    b.ToTable("StoreSections");
                });

            modelBuilder.Entity("MealSuggestionTagMapping", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestion", null)
                        .WithMany()
                        .HasForeignKey("MealSuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestionTag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodCategory", "FoodCategory")
                        .WithMany("FoodItems")
                        .HasForeignKey("FoodCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodCategory");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItemStoreSection", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("StoreSections")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.StoreSection", "StoreSection")
                        .WithMany("FoodItems")
                        .HasForeignKey("StoreSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("StoreSection");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Ingredient", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("Ingredients")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.PriceHistory", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("PriceHistories")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeMealSuggestion", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestion", "MealSuggestion")
                        .WithMany("RecipeSuggestions")
                        .HasForeignKey("MealSuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("MealSuggestions")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealSuggestion");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeStep", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodCategory", b =>
                {
                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("PriceHistories");

                    b.Navigation("StoreSections");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestion", b =>
                {
                    b.Navigation("RecipeSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("MealSuggestions");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.StoreSection", b =>
                {
                    b.Navigation("FoodItems");
                });
#pragma warning restore 612, 618
        }
    }
}

```

## File: 20250205081417_UpdateFoodItem2.cs

```C#
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

```

## File: 20250205081417_UpdateFoodItem2.Designer.cs

```C#
// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyFoodApp.Infrastructure.Persistence;

#nullable disable

namespace MyFoodApp.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250205081417_UpdateFoodItem2")]
    partial class UpdateFoodItem2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MealSuggestionTagMapping", b =>
                {
                    b.Property<int>("MealSuggestionId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("MealSuggestionId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("MealSuggestionTagMapping");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodCategory", b =>
                {
                    b.Property<int>("FoodCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodCategoryId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("FoodCategoryId");

                    b.ToTable("FoodCategories");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.Property<int>("FoodItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodItemId"));

                    b.Property<decimal?>("CaloriesPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CarbohydratesPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<decimal?>("FatPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FoodCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal?>("ProteinPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("FoodItemId");

                    b.HasIndex("FoodCategoryId");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItemStoreSection", b =>
                {
                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<int>("StoreSectionId")
                        .HasColumnType("int");

                    b.Property<int?>("ShelfNumber")
                        .HasColumnType("int");

                    b.HasKey("FoodItemId", "StoreSectionId");

                    b.HasIndex("StoreSectionId");

                    b.ToTable("FoodItemStoreSections");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("IngredientId");

                    b.HasIndex("FoodItemId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestion", b =>
                {
                    b.Property<int>("MealSuggestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealSuggestionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MealType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("MealSuggestionId");

                    b.ToTable("MealSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestionTag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TagId");

                    b.ToTable("MealSuggestionTags");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.PriceHistory", b =>
                {
                    b.Property<int>("PriceHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriceHistoryId"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PriceHistoryId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("PriceHistories");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<int>("CookTimeMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("PrepTimeMinutes")
                        .HasColumnType("int");

                    b.Property<int>("Servings")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeMealSuggestion", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("MealSuggestionId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "MealSuggestionId");

                    b.HasIndex("MealSuggestionId");

                    b.ToTable("RecipeMealSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeStep", b =>
                {
                    b.Property<int>("StepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StepId"));

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("StepNumber")
                        .HasColumnType("int");

                    b.HasKey("StepId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeSteps");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.StoreSection", b =>
                {
                    b.Property<int>("StoreSectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreSectionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StoreSectionId");

                    b.ToTable("StoreSections");
                });

            modelBuilder.Entity("MealSuggestionTagMapping", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestion", null)
                        .WithMany()
                        .HasForeignKey("MealSuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestionTag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodCategory", "FoodCategory")
                        .WithMany("FoodItems")
                        .HasForeignKey("FoodCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodCategory");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItemStoreSection", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("StoreSections")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.StoreSection", "StoreSection")
                        .WithMany("FoodItems")
                        .HasForeignKey("StoreSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("StoreSection");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Ingredient", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("Ingredients")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.PriceHistory", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("PriceHistories")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeMealSuggestion", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestion", "MealSuggestion")
                        .WithMany("RecipeSuggestions")
                        .HasForeignKey("MealSuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("MealSuggestions")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealSuggestion");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeStep", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodCategory", b =>
                {
                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("PriceHistories");

                    b.Navigation("StoreSections");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestion", b =>
                {
                    b.Navigation("RecipeSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("MealSuggestions");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.StoreSection", b =>
                {
                    b.Navigation("FoodItems");
                });
#pragma warning restore 612, 618
        }
    }
}

```

## File: AppDbContextModelSnapshot.cs

```C#
// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyFoodApp.Infrastructure.Persistence;

#nullable disable

namespace MyFoodApp.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MealSuggestionTagMapping", b =>
                {
                    b.Property<int>("MealSuggestionId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("MealSuggestionId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("MealSuggestionTagMapping");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodCategory", b =>
                {
                    b.Property<int>("FoodCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodCategoryId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("FoodCategoryId");

                    b.ToTable("FoodCategories");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.Property<int>("FoodItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodItemId"));

                    b.Property<decimal?>("CaloriesPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CarbohydratesPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<decimal?>("FatPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FoodCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal?>("ProteinPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("FoodItemId");

                    b.HasIndex("FoodCategoryId");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItemStoreSection", b =>
                {
                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<int>("StoreSectionId")
                        .HasColumnType("int");

                    b.Property<int?>("ShelfNumber")
                        .HasColumnType("int");

                    b.HasKey("FoodItemId", "StoreSectionId");

                    b.HasIndex("StoreSectionId");

                    b.ToTable("FoodItemStoreSections");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("IngredientId");

                    b.HasIndex("FoodItemId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestion", b =>
                {
                    b.Property<int>("MealSuggestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealSuggestionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MealType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("MealSuggestionId");

                    b.ToTable("MealSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestionTag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TagId");

                    b.ToTable("MealSuggestionTags");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.PriceHistory", b =>
                {
                    b.Property<int>("PriceHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriceHistoryId"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FoodItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PriceHistoryId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("PriceHistories");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<int>("CookTimeMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("PrepTimeMinutes")
                        .HasColumnType("int");

                    b.Property<int>("Servings")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeMealSuggestion", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("MealSuggestionId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "MealSuggestionId");

                    b.HasIndex("MealSuggestionId");

                    b.ToTable("RecipeMealSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeStep", b =>
                {
                    b.Property<int>("StepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StepId"));

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("StepNumber")
                        .HasColumnType("int");

                    b.HasKey("StepId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeSteps");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.StoreSection", b =>
                {
                    b.Property<int>("StoreSectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreSectionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StoreSectionId");

                    b.ToTable("StoreSections");
                });

            modelBuilder.Entity("MealSuggestionTagMapping", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestion", null)
                        .WithMany()
                        .HasForeignKey("MealSuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestionTag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodCategory", "FoodCategory")
                        .WithMany("FoodItems")
                        .HasForeignKey("FoodCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodCategory");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItemStoreSection", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("StoreSections")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.StoreSection", "StoreSection")
                        .WithMany("FoodItems")
                        .HasForeignKey("StoreSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("StoreSection");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Ingredient", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("Ingredients")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.PriceHistory", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.FoodItem", "FoodItem")
                        .WithMany("PriceHistories")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeMealSuggestion", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.MealSuggestion", "MealSuggestion")
                        .WithMany("RecipeSuggestions")
                        .HasForeignKey("MealSuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("MealSuggestions")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealSuggestion");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.RecipeStep", b =>
                {
                    b.HasOne("MyFoodApp.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodCategory", b =>
                {
                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.FoodItem", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("PriceHistories");

                    b.Navigation("StoreSections");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.MealSuggestion", b =>
                {
                    b.Navigation("RecipeSuggestions");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("MealSuggestions");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("MyFoodApp.Domain.Entities.StoreSection", b =>
                {
                    b.Navigation("FoodItems");
                });
#pragma warning restore 612, 618
        }
    }
}

```

