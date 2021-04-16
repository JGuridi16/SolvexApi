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
            return _context.Cities.OrderBy(c => c.Id).ToList();
        }

        public City GetOne(int cityId)
        {
            return _context.Cities.FirstOrDefault(c => c.Id == cityId);
        }

        public City Create(City city)
        {
            try
            {
                var result = _context.Cities.Add(new City { Name = city.Name, Description = city.Description });
                _context.SaveChanges();

                return result.Entity;
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: "+ex);
                return null;
            }
        }

        public City Update(City cityToUpdate)
        {
            try
            {
                var cityEntity = _context.Cities.FirstOrDefault(city => city.Id == cityToUpdate.Id);
                if (cityEntity == null) return null;
                bool didUpdate = _context.Entry(cityEntity).State == EntityState.Modified;
                if(didUpdate) _context.SaveChanges();

                return cityToUpdate;
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
                return null;
            }
        }

        public City Delete(int id)
        {
            try
            {
                var city = _context.Cities.FirstOrDefault(c => c.Id == id);
                if (city == null) return null;
                _context.Cities.Remove(city);
                _context.SaveChanges();

                return city;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
                return null;
            }
        }
    }
}
