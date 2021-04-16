using BusinessLayer.Dtos.CityEntity;
using BusinessLayer.Interfaces.CityService;
using Microsoft.AspNetCore.Mvc;

namespace SolvexApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet(Name = "GetAllCities")]
        public IActionResult GetCities()
        {
            var cities = _cityService.GetAll();
            return Ok(cities);
        }

        [HttpGet("{id}", Name = "GetCity")]
        public IActionResult GetCity([FromRoute] int id)
        {
            var city = _cityService.GetOne(id);
            if (city == null) return NotFound();

            return Ok(city);
        }

        [HttpPost]
        public IActionResult InsertCity([FromBody] CityDto city)
        {
            var hasRedFlags = HasRedFlags(city.Id);
            if (hasRedFlags) return BadRequest(ModelState);

            var cityResult = _cityService.Create(city);
            if (!cityResult.IsValid)
                return BadRequest("City could not be created");

            return CreatedAtRoute("GetCity", new { city.Id }, city);
        }

        [HttpPut]
        public IActionResult UpdateCity([FromBody] CityDto city)
        {
            var cityResult = _cityService.Update(city);
            if (!cityResult.IsValid)
                return BadRequest(cityResult.Errors);

            return Ok(cityResult.Entity);
        }

        [HttpDelete("{id}")]
        public IActionResult DropCity(int id)
        {
            var cityResult = _cityService.Delete(id);
            if (!cityResult.IsValid)
                return BadRequest(cityResult.Errors);
            return Ok(cityResult.Entity);
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
