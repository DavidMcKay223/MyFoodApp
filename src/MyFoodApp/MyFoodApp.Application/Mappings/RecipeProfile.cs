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
                .ForMember(dest => dest.Recipe, opt => opt.MapFrom(src => src.Recipe))
                .ForMember(dest => dest.MealSuggestion, opt => opt.MapFrom(src => src.MealSuggestion))
                .ReverseMap()
                .ForMember(dest => dest.Recipe, opt => opt.Ignore())
                .ForMember(dest => dest.MealSuggestion, opt => opt.Ignore());

            CreateMap<RecipeStep, RecipeStepDto>()
                .ForMember(dest => dest.Recipe, opt => opt.MapFrom(src => src.Recipe))
                .ReverseMap()
                .ForMember(dest => dest.Recipe, opt => opt.Ignore());
        }
    }
}
