using SolvexApi.Interfaces;
using SolvexApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SolvexApi.Contexts
{
    public class PointsOfInterestDtoContext : IPointsOfInterestContext
    {
        public readonly static IPointsOfInterestContext context = new PointsOfInterestDtoContext();

        public PointsOfInterestDtoContext() { }

        public List<PointOfInterestDto> GetAllPointsOfInterest(CityDto city)
        {
            return city.PointsOfInterest.ToList();
        }

        public PointOfInterestDto GetOnePointOfInterest(CityDto city, int pointOfInterestId)
        {
            var entity = city.PointsOfInterest.SingleOrDefault(e => e.Id == pointOfInterestId);
            return entity;
        }

        public void AttachPointOfInterestToCity(CityDto city, PointOfInterestDto pointOfInterest)
        {
            city.PointsOfInterest.Add(pointOfInterest);
        }

        public bool UpdatePointOfInterest(CityDto city, PointOfInterestDto pointOfInterest)
        {
            var entity = city.PointsOfInterest.SingleOrDefault(p => p.Id == pointOfInterest.Id);
            if (entity == null) return false;

            entity.Id = pointOfInterest.Id;
            entity.Name = pointOfInterest.Name;
            entity.Description = pointOfInterest.Description;

            return true;
        }
        
        public bool DropPointOfInterest(CityDto city, int pointOfInterestId)
        {
            var pointOfInterest = GetOnePointOfInterest(city, pointOfInterestId);
            if (pointOfInterest == null) return false;
            
            city.PointsOfInterest.Remove(pointOfInterest);
            return true;
        }
    }
}