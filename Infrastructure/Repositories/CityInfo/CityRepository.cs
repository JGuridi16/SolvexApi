using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories.CityInfo
{
    public class CityRepository : ICityRepository
    {
        private readonly CityInfoContext _context;

        public CityRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<City> GetAll()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetOne(int cityId)
        {
            return _context.Cities.FirstOrDefault(c => c.Id == cityId);
        }

        public City Create(City city)
        {
            var result = _context.Cities.Add(city);
            Save();

            return result.Entity;
        }

        public City Update(City cityToUpdate)
        {
            var cityEntity = _context.Cities.FirstOrDefault(city => city.Id == cityToUpdate.Id);
            if (cityEntity == null) return null;
            _context.Cities.Update(cityToUpdate);
            Save();

            return cityToUpdate;
        }

        public City Delete(int id)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null) return null;
            _context.Cities.Remove(city);
            Save();

            return city;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
