using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Application.Mappings
{
    public class StoreProfile : Profile
    {
        public StoreProfile() 
        {
            CreateMap<StoreSection, StoreSectionDto>().ReverseMap();
        }
    }
}
