# MyFoodApp 🍔

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://img.shields.io/github/actions/workflow/status/DavidMcKay223/MyFoodApp/dotnet.yml)](https://github.com/DavidMcKay223/MyFoodApp/actions)

## Features ✨
- 🍔 **Food Management**: Track ingredients and recipes.
- 🛒 **Grocery Integration**: Sync with store sections.
- 🤖 **AI Recommendations**: Get meal suggestions.

## Tech Stack 🛠️
- **Frontend**: Blazor 🌀
- **Backend**: .NET 9 🟣
- **Database**: Entity Framework Core 🗃️

## SQL Analytics

### 1. Average Cost per Recipe
```SQL
SELECT 
  r.RecipeId,
  r.Title,
  ROUND(SUM(ph.Price * i.Quantity), 2) AS TotalCost,
  r.Servings,
  ROUND(SUM(ph.Price * i.Quantity)/r.Servings, 2) AS CostPerServing
FROM [MyFoodApp].[dbo].[Recipes] r
JOIN [MyFoodApp].[dbo].[Ingredients] i ON r.RecipeId = i.RecipeId
JOIN (
  SELECT FoodItemId, Price
  FROM [MyFoodApp].[dbo].[PriceHistories]
  WHERE EndDate IS NULL OR EndDate >= GETDATE()
) ph ON i.FoodItemId = ph.FoodItemId
GROUP BY r.RecipeId, r.Title, r.Servings
```

### 2. Popular Meal Suggestion Tags
```SQL
SELECT TOP 10
  t.TagName,
  COUNT(*) AS TagCount
FROM [MyFoodApp].[dbo].[MealSuggestionTagMapping] m
JOIN [MyFoodApp].[dbo].[MealSuggestionTags] t ON m.TagId = t.TagId
GROUP BY t.TagName
ORDER BY TagCount DESC
```

### 3. Current Price Trends by Category
```SQL
SELECT 
  fc.Name AS Category,
  AVG(ph.Price) AS AvgPrice
FROM [MyFoodApp].[dbo].[FoodItems] fi
JOIN [MyFoodApp].[dbo].[PriceHistories] ph ON fi.FoodItemId = ph.FoodItemId
JOIN [MyFoodApp].[dbo].[FoodCategories] fc ON fi.FoodCategoryId = fc.FoodCategoryId
WHERE ph.EndDate >= GETDATE() OR ph.EndDate IS NULL
GROUP BY fc.Name
```

### 4. Recipes by Preparation Time
```SQL
SELECT 
  CASE 
    WHEN (PrepTimeMinutes + CookTimeMinutes) <= 30 THEN 'Quick'
    WHEN (PrepTimeMinutes + CookTimeMinutes) <= 60 THEN 'Moderate'
    ELSE 'Lengthy'
  END AS TimeCategory,
  COUNT(*) AS RecipeCount
FROM [MyFoodApp].[dbo].[Recipes]
GROUP BY 
  CASE 
    WHEN (PrepTimeMinutes + CookTimeMinutes) <= 30 THEN 'Quick'
    WHEN (PrepTimeMinutes + CookTimeMinutes) <= 60 THEN 'Moderate'
    ELSE 'Lengthy'
  END
```

### 5. Store Section Inventory
```SQL
SELECT 
  ss.Name AS StoreSection,
  COUNT(*) AS ItemCount
FROM [MyFoodApp].[dbo].[FoodItemStoreSections] fiss
JOIN [MyFoodApp].[dbo].[StoreSections] ss ON fiss.StoreSectionId = ss.StoreSectionId
GROUP BY ss.Name
```

### 6. High-Protein Recipes
```SQL
SELECT TOP 10
  r.Title,
  SUM(fi.ProteinPerUnit * i.Quantity) AS TotalProtein
FROM [MyFoodApp].[dbo].[Recipes] r
JOIN [MyFoodApp].[dbo].[Ingredients] i ON r.RecipeId = i.RecipeId
JOIN [MyFoodApp].[dbo].[FoodItems] fi ON i.FoodItemId = fi.FoodItemId
GROUP BY r.Title
HAVING SUM(fi.ProteinPerUnit * i.Quantity) > 30
```

### 7. Meal Suggestions Analysis
```SQL
SELECT 
  MealType,
  COUNT(*) AS SuggestionCount,
  AVG(DATEDIFF(DAY, EffectiveDate, ExpirationDate)) AS AvgDurationDays
FROM [MyFoodApp].[dbo].[MealSuggestions]
GROUP BY MealType
```

### 8. Price Fluctuation Analysis
```SQL
SELECT 
  fi.Name AS FoodItem,
  MAX(ph.Price) - MIN(ph.Price) AS PriceFluctuation,
  (MAX(ph.Price) - MIN(ph.Price))/MIN(ph.Price) * 100 AS PercentChange
FROM [MyFoodApp].[dbo].[PriceHistories] ph
JOIN [MyFoodApp].[dbo].[FoodItems] fi ON ph.FoodItemId = fi.FoodItemId
GROUP BY fi.Name
HAVING MAX(ph.Price) - MIN(ph.Price) > 2
```

### 9. Expiring Meal Suggestions
```SQL
SELECT 
  Name,
  Description,
  ExpirationDate
FROM [MyFoodApp].[dbo].[MealSuggestions]
WHERE ExpirationDate BETWEEN GETDATE() AND DATEADD(DAY, 7, GETDATE())
```

### 10. Most Used Ingredients
```SQL
SELECT TOP 15
  fi.Name AS Ingredient,
  COUNT(DISTINCT r.RecipeId) AS RecipeCount
FROM [MyFoodApp].[dbo].[Ingredients] i
JOIN [MyFoodApp].[dbo].[FoodItems] fi ON i.FoodItemId = fi.FoodItemId
JOIN [MyFoodApp].[dbo].[Recipes] r ON i.RecipeId = r.RecipeId
GROUP BY fi.Name
```

## Project Directory Structure
```
└── 📁 MyFoodApp-main/
    ├── 📁 other/
    │   └── 📁 MyFoodApp.ConsoleApp/
    │       └── 🟣 Program.cs
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
    │       │   │   ├── 🟣 FoodItemSearchDto .cs
    │       │   │   ├── 🟣 FoodItemStoreSectionDto.cs
    │       │   │   ├── 🟣 IngredientDto.cs
    │       │   │   ├── 🟣 IngredientSearchDto .cs
    │       │   │   ├── 🟣 MealSuggestionDto.cs
    │       │   │   ├── 🟣 MealSuggestionSearchDto .cs
    │       │   │   ├── 🟣 MealSuggestionTagDto.cs
    │       │   │   ├── 🟣 PriceHistoryDto.cs
    │       │   │   ├── 🟣 PriceHistorySearchDto .cs
    │       │   │   ├── 🟣 RecipeDto.cs
    │       │   │   ├── 🟣 RecipeMealSuggestionDto.cs
    │       │   │   ├── 🟣 RecipeSearchDto .cs
    │       │   │   ├── 🟣 RecipeStepDto.cs
    │       │   │   ├── 🟣 StoreSectionDto.cs
    │       │   │   └── 🟣 StoreSectionSearchDto .cs
    │       │   ├── 📁 Interfaces/
    │       │   │   ├── 📁 Foods/
    │       │   │   │   └── 🟣 IFoodItemUseCases.cs
    │       │   │   ├── 📁 Meals/
    │       │   │   │   └── 🟣 IMealSuggestionUseCases.cs
    │       │   │   ├── 📁 Recipes/
    │       │   │   │   ├── 🟣 IRecipeMealSuggestionUseCases.cs
    │       │   │   │   ├── 🟣 IRecipeStepUseCases.cs
    │       │   │   │   └── 🟣 IRecipeUseCases.cs
    │       │   │   └── 📁 Recommendations/
    │       │   │       └── 🟣 IGenerateRecommendationsUseCases.cs
    │       │   ├── 📁 Mappings/
    │       │   │   ├── 🟣 FoodProfile.cs
    │       │   │   ├── 🟣 IngredientProfile.cs
    │       │   │   ├── 🟣 MealProfile.cs
    │       │   │   ├── 🟣 PriceProfile.cs
    │       │   │   ├── 🟣 RecipeProfile.cs
    │       │   │   └── 🟣 StoreProfile.cs
    │       │   ├── 📁 UseCases/
    │       │   │   ├── 📁 Foods/
    │       │   │   │   └── 🟣 FoodItemUseCases.cs
    │       │   │   ├── 📁 Meals/
    │       │   │   │   └── 🟣 MealSuggestionUseCases.cs
    │       │   │   ├── 📁 Recipes/
    │       │   │   │   └── 🟣 RecipeUseCases.cs
    │       │   │   └── 📁 Recommendations/
    │       │   │       └── 🟣 GenerateRecommendationsUseCases.cs
    │       │   └── 📁 Validators/
    │       │       ├── 🟣 FoodCategoryDtoValidator .cs
    │       │       ├── 🟣 FoodItemDtoValidator .cs
    │       │       ├── 🟣 FoodItemStoreSectionDtoValidator .cs
    │       │       ├── 🟣 IngredientDtoValidator .cs
    │       │       ├── 🟣 MealSuggestionDtoValidator .cs
    │       │       ├── 🟣 MealSuggestionTagDtoValidator .cs
    │       │       ├── 🟣 PriceHistoryDtoValidator .cs
    │       │       ├── 🟣 RecipeDtoValidator .cs
    │       │       ├── 🟣 RecipeMealSuggestionDtoValidator .cs
    │       │       ├── 🟣 RecipeStepDtoValidator .cs
    │       │       └── 🟣 StoreSectionDtoValidator .cs
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
    │       │   │   └── 🟣 AppDbContextModelSnapshot.cs
    │       │   ├── 📁 Persistence/
    │       │   │   └── 🟣 AppDbContext.cs
    │       │   └── 📁 Repositories/
    │       │       ├── 🟣 FoodItemRepository.cs
    │       │       ├── 🟣 MealSuggestionRepository.cs
    │       │       └── 🟣 RecipeRepository .cs
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
    │           │   │   │   ├── 🌀 EditModalRecipe.razor
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
        │   ├── 📁 UseCases/
        │   │   ├── 📁 Foods/
        │   │   │   └── 🟣 FoodItemUseCasesTests.cs
        │   │   ├── 📁 Meals/
        │   │   │   └── 🟣 MealSuggestionUseCasesTests.cs
        │   │   ├── 📁 Recipes/
        │   │   │   └── 🟣 RecipeUseCasesTests.cs
        │   │   └── 📁 Recommendations/
        │   │       └── 🟣 GenerateRecommendationsUseCasesTests.cs
        │   ├── 📁 Validators/
        │   │   ├── 🟣 FoodItemDtoValidatorTests.cs
        │   │   └── 🟣 RecipeDtoValidatorTests.cs
        │   └── 🟣 ApplicationTestFixture.cs
        └── 📁 MyFoodApp.Infrastructure.Tests/
            ├── 📁 Data/
            │   └── 🟣 TestDataFactory.cs
            ├── 📁 Helpers/
            │   └── 🟣 DbContextHelper.cs
            ├── 📁 Repositories/
            │   ├── 🟣 FoodItemRepositoryTests.cs
            │   ├── 🟣 MealSuggestionRepositoryTests.cs
            │   └── 🟣 RecipeRepositoryTests.cs
            └── 🟣 SqliteTestBase.cs
```
