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
    public class FoodProfile : Profile
    {
        public FoodProfile()
        {
            CreateMap<FoodCategory, FoodCategoryDto>().ReverseMap();
            CreateMap<FoodItem, FoodItemDto>().ReverseMap();
            CreateMap<FoodItemStoreSection, FoodItemStoreSectionDto>().ReverseMap();

            CreateMap<FoodItem, FoodItemDto>()
                .ForMember(dest => dest.FoodCategoryId,
                    opt => opt.MapFrom(src => src.FoodCategory.FoodCategoryId))
                .ReverseMap()
                .ForPath(src => src.FoodCategory.FoodCategoryId,
                    opt => opt.MapFrom(dest => dest.FoodCategoryId));
        }
    }
}
