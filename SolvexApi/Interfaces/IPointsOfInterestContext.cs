using System.Collections.Generic;

namespace SolvexApi.Interfaces
{
    public interface IPointsOfInterestContext
    {
        List<PointOfInterestDto> GetAllPointsOfInterest(CityDto city);
        PointOfInterestDto GetOnePointOfInterest(CityDto city, int pointOfInterestId);
        void AttachPointOfInterestToCity(CityDto city, PointOfInterestDto pointOfInterest);
        bool UpdatePointOfInterest(CityDto city, PointOfInterestDto pointOfInterest);
        bool DropPointOfInterest(CityDto city, int pointOfInterestId);
    }
}