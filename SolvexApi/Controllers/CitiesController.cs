using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolvexApi.Contexts;
using SolvexApi.Interfaces;
using SolvexApi.Models;
using System;

namespace SolvexApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityContext context;
        private readonly ILogger _logger;

        public CitiesController(ILogger<CitiesController> logger)
        {
            context = CityDtoContext.context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet(Name = "GetAllCities")]
        public IActionResult GetCities()
        {
            var list = context.GetAllCities();
            if (list == null) return NoContent();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var entityToReturn = context.GetOneCity(id);
            if (entityToReturn == null) return NotFound();
            return Ok(entityToReturn);
        }

        [HttpPost]
        public IActionResult InsertCity([FromBody] CityDto city)
        {
            bool hasRedFlags = HasRedFlags(city.Id);
            if (hasRedFlags) return BadRequest(ModelState);

            var entity = context.GetOneCity(city.Id);
            if (entity != null) return Conflict("City already exists");
            context.InsertCity(city);
            return CreatedAtRoute("GetAllCities", new { }, city);
        }

        [HttpPut]
        public IActionResult UpdateCity([FromBody] CityDto city)
        {
            var isSuccess = context.UpdateCity(city);
            if (!isSuccess) return NotFound(context.GetAllCities());
            return Ok(context.GetAllCities());
        }

        [HttpDelete("{id}")]
        public IActionResult DropCity(int id)
        {
            bool isSuccess = context.DeleteCity(id);
            if (!isSuccess) return NotFound(context.GetAllCities());
            return Ok(context.GetAllCities());
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
