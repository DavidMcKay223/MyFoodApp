using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Application.Mappings
{
    public class MealProfile : Profile
    {
        public MealProfile() 
        {
            CreateMap<MealSuggestion, MealSuggestionDto>().ReverseMap();
            CreateMap<MealSuggestionTag, MealSuggestionTagDto>().ReverseMap();
        }
    }
}
