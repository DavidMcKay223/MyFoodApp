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
