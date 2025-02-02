﻿// <auto-generated />
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
