using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Application.Mappings
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>().ReverseMap();

            CreateMap<Ingredient, IngredientDto>()
                .ForMember(dest => dest.Unit,
                    opt => opt.MapFrom(src => src.Unit))
                .ReverseMap();
        }
    }
}
