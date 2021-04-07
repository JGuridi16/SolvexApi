using AutoMapper;
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
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllCities")]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetAllCities();

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        public IActionResult GetCity([FromRoute] int id, bool includePointsOfInterest = false)
        {
            var city = _cityInfoRepository.GetOneCity(id, includePointsOfInterest);
            if (city == null) return NotFound();
            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }

        [HttpPost]
        public IActionResult InsertCity([FromBody] CityDto city)
        {
            var hasRedFlags = HasRedFlags(city.Id);
            if (hasRedFlags) return BadRequest(ModelState);

            var cityExists = _cityInfoRepository.CityExists(city.Id);

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
        private bool HasRedFlags(int cityId)
        {
            if (cityId <= 0)
            {
                ModelState.AddModelError("City Id", "The id provided is not allowed");
                return true;
            }

            if (!ModelState.IsValid) return true;
            return false;
        }
        #endregion
    }
}
