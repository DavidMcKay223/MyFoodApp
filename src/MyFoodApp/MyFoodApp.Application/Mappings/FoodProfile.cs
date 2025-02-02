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
                .ForMember(dest => dest.FoodItems, opt => opt.MapFrom(src => src.FoodItems))
                .ReverseMap();

            CreateMap<FoodItem, FoodItemDto>()
                .ForMember(dest => dest.FoodCategory, opt => opt.MapFrom(src => src.FoodCategory))
                .ForMember(dest => dest.PriceHistories, opt => opt.MapFrom(src => src.PriceHistories))
                .ForMember(dest => dest.StoreSections, opt => opt.MapFrom(src => src.StoreSections))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ReverseMap();

            CreateMap<FoodItemStoreSection, FoodItemStoreSectionDto>()
                .ForMember(dest => dest.FoodItem, opt => opt.MapFrom(src => src.FoodItem))
                .ForMember(dest => dest.StoreSection, opt => opt.MapFrom(src => src.StoreSection))
                .ReverseMap();
        }
    }
}
