using AutoMapper;
using MyFoodApp.Application.DTOs;
using MyFoodApp.Domain.Entities;

namespace MyFoodApp.Application.Mappings
{
    public class PriceProfile : Profile
    {
        public PriceProfile() 
        {
            CreateMap<PriceHistory, PriceHistoryDto>().ReverseMap();
        }
    }
}
