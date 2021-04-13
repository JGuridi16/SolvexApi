using AutoMapper;
using BusinessLayer.Dtos.PointOfInterestEntity;
using DataAccess.Entities;

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
