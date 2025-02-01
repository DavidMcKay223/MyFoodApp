# Food Application

## Project Structure
```text
MyFoodApp/
├── Application/
│   ├── Common/
│   │   ├── AbstractSearchDto.cs
│   │   ├── ErrorDto.cs
│   │   ├── ResultDto.cs (Has ErrorList with List<Dto> or Item<Dto>)
│   ├── Configurations/
│   │   ├── AutoMapperConfiguration.cs
│   ├── Mappings/
│   │   ├── FoodItemProfile.cs
│   ├── Validators/
│   │   ├── FoodItemDtoValidator.cs
│   ├── DTOs/
│   │   ├── FoodCategoryDto.cs
│   │   ├── FoodItemDto.cs
│   │   ├── FoodItemSearchDto.cs : AbstractSearchDto
│   │   ├── PriceHistoryDto.cs
│   │   ├── StoreSectionDto.cs
│   │   ├── FoodItemStoreSectionDto.cs
│   │   ├── RecipeDto.cs
│   │   ├── IngredientDto.cs
│   │   ├── RecipeStepDto.cs
│   │   ├── MealSuggestionDto.cs
│   │   ├── RecipeMealSuggestionDto.cs
│   │   └── MealSuggestionTagDto.cs
│   ├── Services/
│   │   ├── FoodItemService.cs
│   │   ├── StoreService.cs
│   │   ├── RecipeService.cs
│   │   └── RecommendationService.cs
│   ├── UseCases/
│   │   ├── FoodItems/
│   │   │   └── FoodItemHandler.cs
│   │   ├── Stores/
│   │   │   ├── GetStoreDetailsHandler.cs
│   │   │   └── ListStoresHandler.cs
│   │   ├── Recipes/
│   │   │   └── RecipeHandler.cs
│   │   └── Recommendations/
│   │       └── GenerateRecommendationsHandler.cs
│   ├── Interfaces/
│   │   ├── FoodItems/
│   │   │   └── IFoodItemHandler.cs
│   │   ├── Stores/
│   │   │   ├── IGetStoreDetails.cs
│   │   │   └── IListStores.cs
│   │   ├── Recipes/
│   │   │   └── IRecipeHandler.cs
│   │   └── Recommendations/
│   │       └── IGenerateRecommendations.cs
├── Domain/
│   ├── Entities/
│   │   ├── FoodCategory.cs
│   │   ├── FoodItem.cs
│   │   ├── PriceHistory.cs
│   │   ├── StoreSection.cs
│   │   ├── FoodItemStoreSection.cs
│   │   ├── Recipe.cs
│   │   ├── Ingredient.cs
│   │   ├── RecipeStep.cs
│   │   ├── MealSuggestion.cs
│   │   ├── RecipeMealSuggestion.cs
│   │   └── MealSuggestionTag.cs
│   ├── Exceptions/
│   │   ├── BadRequestException.cs
│   │   ├── NotFoundException.cs
│   ├── Interfaces/
│   │   ├── Repositories/
│   │   │   ├── IFoodItemRepository.cs
│   │   │   └── IStoreRepository.cs
│   │   └── Services/ (if needed for domain-specific services)
│   └── Enums/
│       ├── UnitType.cs
│       └── MealType.cs
├── Infrastructure/
│   ├── Persistence/
│   │   ├── MyFoodAppDbContext.cs
│   ├── Repositories/
│   │   ├── FoodItemRepository.cs
│   │   └── StoreRepository.cs
│   └── ExternalServices/
│       └── ExternalDataService.cs
│   └── Logging/
│       └── ApplicationLogger.cs
├── Shared/
│   ├── Utilities/
│   │   └── UtilityClass.cs
│   ├── Constants/
│   │   └── AppConstants.cs
│   └── Settings/
│       └── JwtSettings.cs
├── WebApp/  (Blazor WebAssembly Project)
│   ├── wwwroot/
│   ├── Pages/
│   │   └── Index.razor
│   │   └── Authentication/
│   │       └── Login.razor
│   │       └── Register.razor
│   ├── Shared/
│   │   ├── MainLayout.razor
│   ├── App.razor
│   ├── Program.cs
│   ├── appsettings.json
Authentication/
├── Application/
│   ├── DTOs/
│   │   ├── UserDto.cs
│   └── UseCases/
│       ├── LoginHandler.cs
│       ├── RegisterHandler.cs
│       ├── ValidateTokenHandler.cs
│   ├── Interfaces/
│   │   ├── ILoginHandler.cs
│   │   ├── IRegisterHandler.cs
│   │   └── IValidateTokenHandler.cs
├── Domain/
│   ├── Entities/
│   │   ├── User.cs
│   ├── Interfaces/
│   │   ├── Repositories/
│   │   │   ├── IUserRepository.cs
│   └── Services/
│       ├── IAuthenticationService.cs
│   └── Enums/
│       └── UserRole.cs
├── Infrastructure/
│   ├── Persistence/
│   │   ├── AuthenticationDbContext.cs
│   │   ├── EntityConfigurations/
│   │       ├── UserConfiguration.cs
│   ├── Repositories/
│   │   ├── UserRepository.cs
│   └── ExternalServices/
│       └── JwtTokenGenerator.cs
├── Configuration/
│   └── AuthConfiguration.cs
```
