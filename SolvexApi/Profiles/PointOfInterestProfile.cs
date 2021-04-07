using AutoMapper;
using SolvexApi.Entities;
using SolvexApi.Models;

namespace SolvexApi.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<PointOfInterest, PointOfInterestDto>();
            CreateMap<PointOfInterestDto, PointOfInterest>();
            CreateMap<PointOfInterestDto, PointOfInterest>().ReverseMap();
        }
    }
}
