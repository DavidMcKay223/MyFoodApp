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
