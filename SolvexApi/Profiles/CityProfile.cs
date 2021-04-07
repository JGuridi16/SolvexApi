using AutoMapper;
using SolvexApi.Entities;
using SolvexApi.Models;

namespace SolvexApi.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityWithoutPointsOfInterestDto>();
            CreateMap<City, CityDto>();
        }
    }
}
