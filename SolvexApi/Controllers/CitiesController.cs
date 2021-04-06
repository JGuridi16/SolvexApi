using Microsoft.AspNetCore.Mvc;
using SolvexApi.Entities;
using SolvexApi.Interfaces;
using SolvexApi.Models;
using System.Collections.Generic;

namespace SolvexApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet(Name = "GetAllCities")]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetAllCities();

            var results = new List<CityWithoutPointOfInterestDto>();

            foreach (var city in cityEntities)
            {
                results.Add(new CityWithoutPointOfInterestDto()
                { 
                    Id = city.Id,
                    Name = city.Name,
                    Description =  city.Description,
                });
            }

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = _cityInfoRepository.GetOneCity(id, includePointsOfInterest);
            if (city == null) return NotFound();
            if (includePointsOfInterest)
            {
                var cityDto = new CityDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                };

                AttachPointsOfInterestToCity(city, ref cityDto);

                return Ok(cityDto);
            }

            var cityWithoutPointsResult = new CityWithoutPointOfInterestDto()
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description
            };

            return Ok(cityWithoutPointsResult);
        }

        [HttpPost]
        public IActionResult InsertCity([FromBody] CityDto city)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCity([FromBody] CityDto city)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DropCity(int id)
        {
            return Ok();
        }

        #region Private Methods
        private void AttachPointsOfInterestToCity(City cityFromRepo, ref CityDto cityDto)
        {
            foreach (var pointOfInterest in cityFromRepo.PointsOfInterest)
            {
                cityDto.PointsOfInterest.Add(new PointOfInterestDto()
                {
                    Id = pointOfInterest.Id,
                    Name = pointOfInterest.Name,
                    Description = pointOfInterest.Description
                });
            }
        }
        #endregion
    }
}
