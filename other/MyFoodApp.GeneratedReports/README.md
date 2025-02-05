# Project Directory Structure Overview

```
├── 📁 .github/
│   └── 📁 workflows/
├── 📁 other/
│   ├── 📁 MyFoodApp.ConsoleApp/
│   │   ├── 📁 Resource/
│   │   └── 🟣 Program.cs
│   └── 📁 MyFoodApp.GeneratedReports/
├── 📁 src/
│   └── 📁 MyFoodApp/
│       ├── 📁 MyFoodApp.Application/
│       │   ├── 📁 Common/
│       │   │   ├── 🟣 Error.cs
│       │   │   ├── 🟣 Response.cs
│       │   │   └── 🟣 SearchDto.cs
│       │   ├── 📁 Configurations/
│       │   │   └── 🟣 AutoMapperConfiguration.cs
│       │   ├── 📁 DTOs/
│       │   │   ├── 🟣 FoodCategoryDto.cs
│       │   │   ├── 🟣 FoodItemDto.cs
│       │   │   ├── 🟣 FoodItemSearchDto.cs
│       │   │   ├── 🟣 FoodItemStoreSectionDto.cs
│       │   │   ├── 🟣 IngredientDto.cs
│       │   │   ├── 🟣 IngredientSearchDto.cs
│       │   │   ├── 🟣 MealSuggestionDto.cs
│       │   │   ├── 🟣 MealSuggestionSearchDto.cs
│       │   │   ├── 🟣 MealSuggestionTagDto.cs
│       │   │   ├── 🟣 PriceHistoryDto.cs
│       │   │   ├── 🟣 PriceHistorySearchDto.cs
│       │   │   ├── 🟣 RecipeDto.cs
│       │   │   ├── 🟣 RecipeMealSuggestionDto.cs
│       │   │   ├── 🟣 RecipeSearchDto.cs
│       │   │   ├── 🟣 RecipeStepDto.cs
│       │   │   ├── 🟣 StoreSectionDto.cs
│       │   │   └── 🟣 StoreSectionSearchDto.cs
│       │   ├── 📁 Interfaces/
│       │   │   ├── 🟣 IFoodItemUseCases.cs
│       │   │   ├── 🟣 IGenerateRecommendationsUseCases.cs
│       │   │   ├── 🟣 IMealSuggestionUseCases.cs
│       │   │   ├── 🟣 INutritionCalculatorService.cs
│       │   │   └── 🟣 IRecipeUseCases.cs
│       │   ├── 📁 Mappings/
│       │   │   ├── 🟣 FoodProfile.cs
│       │   │   ├── 🟣 IngredientProfile.cs
│       │   │   ├── 🟣 MealProfile.cs
│       │   │   ├── 🟣 PriceProfile.cs
│       │   │   ├── 🟣 RecipeProfile.cs
│       │   │   └── 🟣 StoreProfile.cs
│       │   ├── 📁 UseCases/
│       │   │   ├── 🟣 FoodItemUseCases.cs
│       │   │   ├── 🟣 GenerateRecommendationsUseCases.cs
│       │   │   ├── 🟣 MealSuggestionUseCases.cs
│       │   │   ├── 🟣 NutritionCalculatorService.cs
│       │   │   └── 🟣 RecipeUseCases.cs
│       │   └── 📁 Validators/
│       │       ├── 🟣 FoodCategoryDtoValidator.cs
│       │       ├── 🟣 FoodItemDtoValidator.cs
│       │       ├── 🟣 FoodItemStoreSectionDtoValidator.cs
│       │       ├── 🟣 IngredientDtoValidator.cs
│       │       ├── 🟣 MealSuggestionDtoValidator.cs
│       │       ├── 🟣 MealSuggestionTagDtoValidator.cs
│       │       ├── 🟣 PriceHistoryDtoValidator.cs
│       │       ├── 🟣 RecipeDtoValidator.cs
│       │       ├── 🟣 RecipeMealSuggestionDtoValidator.cs
│       │       ├── 🟣 RecipeStepDtoValidator.cs
│       │       └── 🟣 StoreSectionDtoValidator.cs
│       ├── 📁 MyFoodApp.Domain/
│       │   ├── 📁 Entities/
│       │   │   ├── 🟣 FoodCategory.cs
│       │   │   ├── 🟣 FoodItem.cs
│       │   │   ├── 🟣 FoodItemStoreSection.cs
│       │   │   ├── 🟣 Ingredient.cs
│       │   │   ├── 🟣 MealSuggestion.cs
│       │   │   ├── 🟣 MealSuggestionTag.cs
│       │   │   ├── 🟣 PriceHistory.cs
│       │   │   ├── 🟣 Recipe.cs
│       │   │   ├── 🟣 RecipeMealSuggestion.cs
│       │   │   ├── 🟣 RecipeStep.cs
│       │   │   └── 🟣 StoreSection.cs
│       │   ├── 📁 Enums/
│       │   │   ├── 🟣 FoodItemSortBy.cs
│       │   │   ├── 🟣 IngredientSortBy.cs
│       │   │   ├── 🟣 MealSuggestionSortBy.cs
│       │   │   ├── 🟣 MealType.cs
│       │   │   ├── 🟣 PriceHistorySortBy.cs
│       │   │   ├── 🟣 RecipeSortBy.cs
│       │   │   ├── 🟣 SortOrder.cs
│       │   │   ├── 🟣 StoreSectionSortBy.cs
│       │   │   └── 🟣 UnitType.cs
│       │   └── 📁 Interfaces/
│       │       └── 📁 Repositories/
│       │           ├── 🟣 IFoodItemRepository.cs
│       │           ├── 🟣 IMealSuggestionRepository.cs
│       │           └── 🟣 IRecipeRepository.cs
│       ├── 📁 MyFoodApp.Infrastructure/
│       │   ├── 📁 Migrations/
│       │   │   ├── 🟣 20250202045404_InitialMigration.cs
│       │   │   ├── 🟣 20250202045404_InitialMigration.Designer.cs
│       │   │   ├── 🟣 20250205074814_UpdateFoodItem.cs
│       │   │   ├── 🟣 20250205074814_UpdateFoodItem.Designer.cs
│       │   │   ├── 🟣 20250205081417_UpdateFoodItem2.cs
│       │   │   ├── 🟣 20250205081417_UpdateFoodItem2.Designer.cs
│       │   │   └── 🟣 AppDbContextModelSnapshot.cs
│       │   ├── 📁 Persistence/
│       │   │   └── 🟣 AppDbContext.cs
│       │   └── 📁 Repositories/
│       │       ├── 🟣 FoodItemRepository.cs
│       │       ├── 🟣 MealSuggestionRepository.cs
│       │       └── 🟣 RecipeRepository.cs
│       ├── 📁 MyFoodApp.Shared/
│       │   └── 📁 Settings/
│       │       └── 🟣 JwtSettings.cs
│       └── 📁 MyFoodApp.Web/
│           ├── 📁 Components/
│           │   ├── 📁 Authentication/
│           │   │   ├── 🌀 Login.razor
│           │   │   └── 🌀 Register.razor
│           │   ├── 📁 Layout/
│           │   │   ├── 🌀 MainLayout.razor
│           │   │   └── 🌀 NavMenu.razor
│           │   ├── 📁 Pages/
│           │   │   ├── 📁 Recipes/
│           │   │   │   ├── 🌀 DisplayModalRecipe.razor
│           │   │   │   ├── 🌀 EditModalRecipe.razor
│           │   │   │   ├── 🌀 FormDisplayIngredient.razor
│           │   │   │   ├── 🌀 FormDisplayRecipeMealSuggestion.razor
│           │   │   │   ├── 🌀 FormDisplayRecipeStep.razor
│           │   │   │   ├── 🌀 FormEditIngredient.razor
│           │   │   │   ├── 🌀 FormEditRecipeMealSuggestion.razor
│           │   │   │   ├── 🌀 FormEditRecipeStep.razor
│           │   │   │   ├── 🌀 FormEditSelectFoodItem.razor
│           │   │   │   ├── 🌀 FormEditSelectMealSuggestion.razor
│           │   │   │   ├── 🌀 IndexDisplayRecipe.razor
│           │   │   │   ├── 🌀 IndexEditRecipe.razor
│           │   │   │   ├── 🌀 ListDisplayRecipe.razor
│           │   │   │   ├── 🌀 ListEditRecipe.razor
│           │   │   │   ├── 🌀 SearchFormRecipe.razor
│           │   │   │   ├── 🌀 TableDisplayRecipe.razor
│           │   │   │   └── 🌀 TableFormRecipe.razor
│           │   │   ├── 🌀 Error.razor
│           │   │   └── 🌀 Home.razor
│           │   ├── 📁 Shared/
│           │   │   ├── 🌀 CustomTimeInput.razor
│           │   │   ├── 🌀 DisplayError.razor
│           │   │   ├── 🌀 SearchButton.razor
│           │   │   └── 🌀 SearchPagination.razor
│           │   ├── 🌀 _Imports.razor
│           │   ├── 🌀 App.razor
│           │   └── 🌀 Routes.razor
│           ├── 📁 Properties/
│           ├── 📁 wwwroot/
│           │   └── 📁 lib/
│           │       └── 📁 bootstrap/
│           │           └── 📁 dist/
│           │               ├── 📁 css/
│           │               └── 📁 js/
│           └── 🟣 Program.cs
└── 📁 test/
    ├── 📁 MyFoodApp.Application.Tests/
    │   ├── 📁 Data/
    │   │   └── 🟣 ApplicationTestDataFactory.cs
    │   ├── 📁 UseCases/
    │   │   ├── 🟣 FoodItemUseCasesTests.cs
    │   │   ├── 🟣 GenerateRecommendationsUseCasesTests.cs
    │   │   ├── 🟣 MealSuggestionUseCasesTests.cs
    │   │   └── 🟣 RecipeUseCasesTests.cs
    │   ├── 📁 Validators/
    │   │   ├── 🟣 FoodCategoryDtoValidatorTests.cs
    │   │   ├── 🟣 FoodItemDtoValidatorTests.cs
    │   │   ├── 🟣 FoodItemStoreSectionDtoValidatorTests.cs
    │   │   ├── 🟣 IngredientDtoValidatorTests.cs
    │   │   ├── 🟣 MealSuggestionDtoValidatorTests.cs
    │   │   ├── 🟣 MealSuggestionTagDtoValidatorTests.cs
    │   │   ├── 🟣 PriceHistoryDtoValidatorTests.cs
    │   │   ├── 🟣 RecipeDtoValidatorTests.cs
    │   │   ├── 🟣 RecipeMealSuggestionDtoValidatorTests.cs
    │   │   ├── 🟣 RecipeStepDtoValidatorTests.cs
    │   │   └── 🟣 StoreSectionDtoValidatorTests.cs
    │   └── 🟣 ApplicationTestFixture.cs
    └── 📁 MyFoodApp.Infrastructure.Tests/
        ├── 📁 Data/
        │   └── 🟣 DomainTestDataFactory.cs
        ├── 📁 Helpers/
        │   └── 🟣 DbContextHelper.cs
        └── 📁 Repositories/
            ├── 🟣 FoodItemRepositoryTests.cs
            ├── 🟣 MealSuggestionRepositoryTests.cs
            └── 🟣 RecipeRepositoryTests.cs
```
