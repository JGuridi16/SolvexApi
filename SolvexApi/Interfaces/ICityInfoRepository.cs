using SolvexApi.Entities;
using System.Collections.Generic;

namespace SolvexApi.Interfaces
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetAllCities();
        City GetOneCity(int cityId, bool includePointsOfInterest);
        IEnumerable<PointOfInterest> GetAllPointsOfInterestForCity(int cityId);
        PointOfInterest GetOnePointOfInterestForCity(int cityId, int pointOfInterestId);
        bool CityExists(int cityId);
    }
}
