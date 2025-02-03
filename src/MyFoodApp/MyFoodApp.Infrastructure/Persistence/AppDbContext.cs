using Microsoft.EntityFrameworkCore;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<FoodItemStoreSection> FoodItemStoreSections { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<MealSuggestion> MealSuggestions { get; set; }
        public DbSet<MealSuggestionTag> MealSuggestionTags { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeMealSuggestion> RecipeMealSuggestions { get; set; }
        public DbSet<RecipeStep> RecipeSteps { get; set; }
        public DbSet<StoreSection> StoreSections { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection", options =>
                    options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite keys for join tables
            modelBuilder.Entity<FoodItemStoreSection>()
                .HasKey(fss => new { fss.FoodItemId, fss.StoreSectionId });

            modelBuilder.Entity<RecipeMealSuggestion>()
                .HasKey(rms => new { rms.RecipeId, rms.MealSuggestionId });

            // Configure many-to-many relationships
            modelBuilder.Entity<MealSuggestion>()
                .HasMany(ms => ms.Tags)
                .WithMany(mt => mt.MealSuggestions)
                .UsingEntity<Dictionary<string, object>>(
                    "MealSuggestionTagMapping",
                    j => j.HasOne<MealSuggestionTag>().WithMany().HasForeignKey("TagId"),
                    j => j.HasOne<MealSuggestion>().WithMany().HasForeignKey("MealSuggestionId")
                );

            // Configure one-to-many relationships
            modelBuilder.Entity<FoodItem>()
                .HasOne(fi => fi.FoodCategory)
                .WithMany(fc => fc.FoodItems)
                .HasForeignKey(fi => fi.FoodCategoryId);

            modelBuilder.Entity<PriceHistory>()
                .HasOne(ph => ph.FoodItem)
                .WithMany(fi => fi.PriceHistories)
                .HasForeignKey(ph => ph.FoodItemId);

            modelBuilder.Entity<RecipeStep>()
                .HasOne(rs => rs.Recipe)
                .WithMany(r => r.Steps)
                .HasForeignKey(rs => rs.RecipeId);

            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(i => i.RecipeId);

            // Configure navigation properties for join entities
            modelBuilder.Entity<FoodItemStoreSection>()
                .HasOne(fss => fss.FoodItem)
                .WithMany(fi => fi.StoreSections)
                .HasForeignKey(fss => fss.FoodItemId);

            modelBuilder.Entity<FoodItemStoreSection>()
                .HasOne(fss => fss.StoreSection)
                .WithMany(ss => ss.FoodItems)
                .HasForeignKey(fss => fss.StoreSectionId);

            modelBuilder.Entity<RecipeMealSuggestion>()
                .HasOne(rms => rms.Recipe)
                .WithMany(r => r.MealSuggestions)
                .HasForeignKey(rms => rms.RecipeId);

            modelBuilder.Entity<RecipeMealSuggestion>()
                .HasOne(rms => rms.MealSuggestion)
                .WithMany(ms => ms.RecipeSuggestions)
                .HasForeignKey(rms => rms.MealSuggestionId);
        }

        [Obsolete("Do not use", true)]
        public void DetachAllEntities()
        {
            this.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Detached);
        }
    }
}
