# Directory: Mappings

## File: FoodProfile.cs

```C#
using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Application.Mappings
{
    public class FoodProfile : Profile
    {
        public FoodProfile()
        {
            CreateMap<FoodCategory, FoodCategoryDto>()
                .ReverseMap();

            CreateMap<FoodItem, FoodItemDto>()
                .ForMember(dest => dest.FoodCategory, opt => opt.MapFrom(src => src.FoodCategory))
                .ReverseMap();

            CreateMap<FoodItemStoreSection, FoodItemStoreSectionDto>()
                .ForMember(dest => dest.FoodItem, opt => opt.MapFrom(src => src.FoodItem))
                .ForMember(dest => dest.StoreSection, opt => opt.MapFrom(src => src.StoreSection))
                .ReverseMap();
        }
    }
}

```

## File: IngredientProfile.cs

```C#
using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Application.Mappings
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>()
                .ForMember(dest => dest.FoodItem, opt => opt.MapFrom(src => src.FoodItem))
                .ReverseMap()
                .ForMember(dest => dest.Recipe, opt => opt.Ignore())
                .ForMember(dest => dest.FoodItem, opt => opt.Ignore());
        }
    }
}

```

## File: MealProfile.cs

```C#
using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Application.Mappings
{
    public class MealProfile : Profile
    {
        public MealProfile()
        {
            CreateMap<MealSuggestion, MealSuggestionDto>()
                .ForMember(dest => dest.RecipeSuggestions, opt => opt.MapFrom(src => src.RecipeSuggestions))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ReverseMap();

            CreateMap<MealSuggestionTag, MealSuggestionTagDto>()
                .ForMember(dest => dest.MealSuggestions, opt => opt.MapFrom(src => src.MealSuggestions))
                .ReverseMap();
        }
    }
}

```

## File: PriceProfile.cs

```C#
using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Application.Mappings
{
    public class PriceProfile : Profile
    {
        public PriceProfile()
        {
            CreateMap<PriceHistory, PriceHistoryDto>()
                .ForMember(dest => dest.FoodItem, opt => opt.MapFrom(src => src.FoodItem))
                .ReverseMap();
        }
    }
}

```

## File: RecipeProfile.cs

```C#
using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Application.Mappings
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.MealSuggestions, opt => opt.MapFrom(src => src.MealSuggestions))
                .ReverseMap()
                .ForMember(dest => dest.Steps, opt => opt.Ignore())
                .ForMember(dest => dest.Ingredients, opt => opt.Ignore())
                .ForMember(dest => dest.MealSuggestions, opt => opt.Ignore());

            CreateMap<RecipeMealSuggestion, RecipeMealSuggestionDto>()
                .ForMember(dest => dest.Recipe, opt => opt.MapFrom(src => new RecipeDto
                {
                    // Copy only top-level properties of Recipe
                    RecipeId = src.RecipeId,
                    Title = src.Recipe.Title,
                    Description = src.Recipe.Description,
                    CookTimeMinutes = src.Recipe.CookTimeMinutes,
                    PrepTimeMinutes = src.Recipe.PrepTimeMinutes,
                    Servings = src.Recipe.Servings,
                }))
                .ForMember(dest => dest.MealSuggestion, opt => opt.MapFrom(src => src.MealSuggestion))
                .ReverseMap()
                .ForMember(dest => dest.Recipe, opt => opt.Ignore())
                .ForMember(dest => dest.MealSuggestion, opt => opt.Ignore());

            CreateMap<RecipeStep, RecipeStepDto>()
                .ReverseMap()
                .ForMember(dest => dest.Recipe, opt => opt.Ignore());
        }
    }
}

```

## File: StoreProfile.cs

```C#
using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Application.Mappings
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<StoreSection, StoreSectionDto>()
                .ReverseMap();
        }
    }
}

```

