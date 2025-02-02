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
                .ForMember(dest => dest.FoodItems, opt => opt.MapFrom(src => src.FoodItems))
                .ReverseMap();
        }
    }
}
