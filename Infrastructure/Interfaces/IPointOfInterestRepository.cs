using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IPointOfInterestRepository
    {
        IEnumerable<PointOfInterest> GetAll(int cityId);
        PointOfInterest GetOne(int cityId, int pointOfInterestId);
        void Create(int cityId, PointOfInterest pointOfInterest);
        void Update(int cityId, PointOfInterest pointOfInterest);
        void Delete(int cityId, int pointOfInterestId);
        void Save();
    }
}
