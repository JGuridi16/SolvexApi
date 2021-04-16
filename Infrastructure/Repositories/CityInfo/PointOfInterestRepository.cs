using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories.CityInfo
{
    public class PointOfInterestRepository : IPointOfInterestRepository
    {
        private readonly CityInfoContext _context;

        public PointOfInterestRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<PointOfInterest> GetAll(int cityId)
        {
            return _context.PointsOfInterest.Where(p => p.CityId == cityId)
                .OrderBy(p => p.Id).ToList();
        }

        public PointOfInterest GetOne(int cityId, int pointOfInterestId)
        {
            return _context.PointsOfInterest
                .FirstOrDefault(p => p.CityId == cityId && p.Id == pointOfInterestId);
        }
        public void Create(int cityId, PointOfInterest pointOfInterest)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return;
            city.PointsOfInterest.Add(pointOfInterest);
            Save();
        }

        public void Update(int cityId, PointOfInterest pointOfInterestToUpdate)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return;

            var pointOfInterestResult = _context.PointsOfInterest
                .FirstOrDefault(p => p == pointOfInterestToUpdate);

            if (pointOfInterestToUpdate == null) return;
            _context.PointsOfInterest.Update(pointOfInterestToUpdate);
            Save();
        }

        public void Delete(int cityId, int pointOfInterestId)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return;

            var pointOfInterestResult = _context.PointsOfInterest
                .FirstOrDefault(p => p.Id == pointOfInterestId && p.CityId == cityId);

            if (pointOfInterestResult == null) return;
            _context.PointsOfInterest.Remove(pointOfInterestResult);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
