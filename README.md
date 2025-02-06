# MyFoodApp 🍔

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://img.shields.io/github/actions/workflow/status/DavidMcKay223/MyFoodApp/dotnet.yml)](https://github.com/DavidMcKay223/MyFoodApp/actions)

## Features ✨
- 🍔 **Food Management**: Track ingredients and recipes, and food information.
- 🛒 **Grocery Integration**: Sync with store sections, store lookup along with price checker.
- 🤖 **Meal Recommendations**: Get meal suggestions, based on existing ingredients.

## Tech Stack 🛠️
- **Frontend**: Blazor 🌀
- **Backend**: .NET 9 🟣
- **Database**: Entity Framework Core 🗃️
- **Testing**: XUnit 🧪, Bogus 🎭, Shouldly ✅
- **Mapping**: AutoMapper 🧩
- **Validation**: FluentValidation 🛡️

## Project Directory Structure
[Auto Generated Directory](https://github.com/DavidMcKay223/MyFoodApp/tree/main/other/MyFoodApp.GeneratedReports#readme)

## Project Directory Structure Diagram
```mermaid
graph TD;
    MyFoodApp-main-->src;
    src-->MyFoodApp;
    MyFoodApp-->MyFoodApp.Application;
    MyFoodApp-->MyFoodApp.Domain;
    MyFoodApp-->MyFoodApp.Infrastructure;
    MyFoodApp-->MyFoodApp.Shared;
    MyFoodApp-->MyFoodApp.Web;

    MyFoodApp.Application-->Common;
    MyFoodApp.Application-->Configurations;
    MyFoodApp.Application-->DTOs;
    MyFoodApp.Application-->Interfaces;
    MyFoodApp.Application-->Mappings;
    MyFoodApp.Application-->UseCases;
    MyFoodApp.Application-->Validators;

    MyFoodApp.Domain-->Entities;
    MyFoodApp.Domain-->Enums;
    MyFoodApp.Domain-->Interfaces;

    MyFoodApp.Infrastructure-->Migrations;
    MyFoodApp.Infrastructure-->Persistence;
    MyFoodApp.Infrastructure-->Repositories;

    MyFoodApp.Shared-->Settings;

    MyFoodApp.Web-->Components;
    MyFoodApp.Web-->Pages;
    MyFoodApp.Web-->Shared;
    MyFoodApp.Web-->Properties;
    MyFoodApp.Web-->wwwroot;

    Components-->Authentication;
    Components-->Layout;

    Pages-->Recipes;

    wwwroot-->lib;
    lib-->bootstrap;
    bootstrap-->dist;
    dist-->css;
    dist-->js;
```

## Class Diagram
```mermaid
classDiagram
    direction TB

    class FoodCategory {
        📁 FoodCategory
        +int FoodCategoryId
        +string Name
        +string Description
        +ICollection~FoodItem~ FoodItems
    }

    class FoodItem {
        🍎 FoodItem
        +int FoodItemId
        +string Name
        +string Description
        +decimal? CaloriesPerUnit
        +decimal? ProteinPerUnit
        +FoodCategory FoodCategory
        +ICollection~PriceHistory~ PriceHistories
        +ICollection~FoodItemStoreSection~ StoreSections
        +ICollection~Ingredient~ Ingredients
    }

    class StoreSection {
        🏪 StoreSection
        +int StoreSectionId
        +string Name
        +string Description
        +ICollection~FoodItemStoreSection~ FoodItems
    }

    class FoodItemStoreSection {
        🔗 Junction
        +int FoodItemId
        +int StoreSectionId
        +int? ShelfNumber
        +FoodItem FoodItem
        +StoreSection StoreSection
    }

    class PriceHistory {
        💰 PriceHistory
        +int PriceHistoryId
        +decimal Price
        +DateTime StartDate
        +DateTime? EndDate
        +FoodItem FoodItem
    }

    class Recipe {
        📜 Recipe
        +int RecipeId
        +string Title
        +string Description
        +int PrepTimeMinutes
        +int CookTimeMinutes
        +int Servings
        +ICollection~Ingredient~ Ingredients
        +ICollection~RecipeStep~ Steps
        +ICollection~RecipeMealSuggestion~ MealSuggestions
    }

    class Ingredient {
        🧂 Ingredient
        +int IngredientId
        +decimal Quantity
        +UnitType Unit
        +Recipe Recipe
        +FoodItem FoodItem
    }

    class RecipeStep {
        📋 Step
        +int StepId
        +int StepNumber
        +string Instruction
        +Recipe Recipe
    }

    class MealSuggestion {
        🍽️ MealSuggestion
        +int MealSuggestionId
        +string Name
        +MealType MealType
        +string Description
        +DateTime EffectiveDate
        +DateTime? ExpirationDate
        +ICollection~RecipeMealSuggestion~ RecipeSuggestions
        +ICollection~MealSuggestionTag~ Tags
    }

    class RecipeMealSuggestion {
        🔗 Junction
        +Recipe Recipe
        +MealSuggestion MealSuggestion
    }

    class MealSuggestionTag {
        🏷️ Tag
        +int TagId
        +string TagName
        +ICollection~MealSuggestion~ MealSuggestions
    }

    FoodCategory "1" --> "0..*" FoodItem : Contains
    FoodItem "1" --> "0..*" PriceHistory : Has history
    FoodItem "1" --> "0..*" FoodItemStoreSection : Located in
    StoreSection "1" --> "0..*" FoodItemStoreSection : Contains
    FoodItem "1" --> "0..*" Ingredient : Used as
    Recipe "1" --> "0..*" Ingredient : Requires
    Recipe "1" --> "0..*" RecipeStep : Has steps
    Recipe "0..*" --> "0..*" MealSuggestion : Suggested in
    MealSuggestion "0..*" --> "0..*" MealSuggestionTag : Tagged with
    MealSuggestion "1" --> "0..*" RecipeMealSuggestion : Includes
    RecipeMealSuggestion "0..*" --> "1" Recipe : Recipes
```
