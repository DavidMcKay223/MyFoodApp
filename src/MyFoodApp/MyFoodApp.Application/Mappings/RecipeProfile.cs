using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .ReverseMap();

            CreateMap<RecipeMealSuggestion, RecipeMealSuggestionDto>()
                .ForMember(dest => dest.Recipe, opt => opt.MapFrom(src => src.Recipe))
                .ForMember(dest => dest.MealSuggestion, opt => opt.MapFrom(src => src.MealSuggestion))
                .ReverseMap();

            CreateMap<RecipeStep, RecipeStepDto>().ReverseMap();
        }
    }
}
