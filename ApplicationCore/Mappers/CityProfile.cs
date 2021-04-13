using AutoMapper;
using BusinessLayer.Dtos.CityEntity;
using DataAccess.Entities;

namespace SolvexApi.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityWithoutPointsOfInterestDto>();
            CreateMap<City, CityDto>();
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
