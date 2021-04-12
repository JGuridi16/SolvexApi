using AutoMapper;
using SolvexApi.Entities;
using SolvexApi.Models;

namespace SolvexApi.Profiles
{
    public class CityValidationProfile : Profile
    {
        public CityValidationProfile()
        {
            CreateMap<City, CityDto>()
                .ForMember(c => c.Description, options => options.MapFrom(source => $"{source.Id} - {source.Name}"))
                .ReverseMap();
        }
    }
}