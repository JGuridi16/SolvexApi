using SolvexApi.Interfaces;
using SolvexApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SolvexApi.Contexts
{
    public class CityDtoContext : ICityContext
    {
        public readonly static ICityContext context = new CityDtoContext();
        private List<CityDto> list;

        public CityDtoContext()
        {
            LoadCities();
        }

        public List<CityDto> GetAllCities()
        {
            return list;
        }

        public CityDto GetOneCity(int id)
        {
            var city = list.SingleOrDefault(c => c.Id.Equals(id));
            return city;
        }

        public void InsertCity(CityDto city)
        {
            list.Add(city);
        }

        public bool UpdateCity(CityDto city)
        {
            var cityFromContext = list.SingleOrDefault(c => c.Id.Equals(city.Id));
            if (cityFromContext == null) return false;
            cityFromContext.Id = city.Id;
            cityFromContext.Name = city.Name;
            cityFromContext.Description = city.Description;
            return true;
        }

        public bool DeleteCity(int id)
        {
            var city = list.SingleOrDefault(c => c.Id.Equals(id));
            if (city == null) return false;
            list.Remove(city);
            return true;
        }

        #region Private Methods
        private void LoadCities()
        {
            list = new List<CityDto>() 
            {
                new CityDto()
                { 
                    Id = 1, 
                    Name = "New York City", 
                    Description = "Best city in the world",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visited urban park in the US"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "A 102-story skycraper located at Midtown Manhattan"
                        },
                    }
                },
                new CityDto()
                { 
                    Id = 2, 
                    Name = "Santo Domingo", 
                    Description = "Hottest city in the world",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Jardin Botanico",
                            Description = "The most visited park in the DR"
                        }
                    }
                }
            };
        }
        #endregion
    }
}