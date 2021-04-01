using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolvexApi.Contexts;
using SolvexApi.Interfaces;
using SolvexApi.Models;
using System;
using System.Linq;

namespace SolvexApi.Controllers
{
    [ApiController]
    [Route("cities/{cityId}/pointsofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ICityContext cityContext;
        private readonly IPointsOfInterestContext pointsOfInterestContext;
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailService _mailService;

        public PointOfInterestController(ILogger<PointOfInterestController> logger, IMailService mailService)
        {
            cityContext = CityDtoContext.context;
            pointsOfInterestContext = PointsOfInterestDtoContext.context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                //throw new Exception();
                var city = cityContext.GetOneCity(cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} was not found when accesing point of interest");
                    return NotFound();
                }

                var list = pointsOfInterestContext.GetAllPointsOfInterest(city);
                return Ok(list);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Execption while getting points of interest for the city with id {cityId}", ex.Message);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = cityContext.GetOneCity(cityId);
            if (city == null) return NotFound("No city found");

            var pointOfInterest = pointsOfInterestContext.GetOnePointOfInterest(city, id);
            if (pointOfInterest == null) return NotFound("No interest point found");
            return Ok(pointOfInterest);
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestDto pointOfInterest)
        {
            bool hasRedFlags = HasRedFlags(pointOfInterest.Id);
            if (hasRedFlags) return BadRequest(ModelState);

            var city = cityContext.GetOneCity(cityId);
            if (city == null) return NotFound("City does not exist");

            var entityExists = CityContainsPointOfInterest(city, pointOfInterest);
            if (entityExists) return Conflict("Point already created");

            pointsOfInterestContext.AttachPointOfInterestToCity(city, pointOfInterest);
            return CreatedAtRoute("GetPointOfInterest", new { cityId, pointOfInterest.Id }, pointOfInterest);
        }

        [HttpPut]
        public IActionResult UpdatePointOfInterest(int cityId, PointOfInterestDto pointOfInterest)
        {
            var city = cityContext.GetOneCity(cityId);
            if (city == null) return NotFound("Invalid city");

            var isSuccess = pointsOfInterestContext.UpdatePointOfInterest(city, pointOfInterest);
            if (!isSuccess) return NotFound("No point of interest found");

            return Ok();
        }

        [HttpDelete("{pointOfInterestId}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = cityContext.GetOneCity(cityId);
            if (city == null) return NotFound("Invalid city");

            var isSuccess = pointsOfInterestContext.DropPointOfInterest(city, pointOfInterestId);
            if (!isSuccess) return NotFound("Point of interest not found");

            _mailService.SendMail("Point of interest deleted", $"Point of interest with id {pointOfInterestId} was deleted.");
            
            return NoContent();
        }

        #region Private Methods

        private bool CityContainsPointOfInterest(CityDto city, PointOfInterestDto pointOfInterest)
        {
            var entity = city.PointsOfInterest.SingleOrDefault(p => p.Id.Equals(pointOfInterest.Id));
            if (entity == null) return false;
            return true;
        }

        private bool HasRedFlags(int pointOfInterestId)
        {
            if (pointOfInterestId <= 0)
            {
                ModelState.AddModelError("Interest Point Id", "The id provided is not allowed");
                return true;
            }
            if (!ModelState.IsValid) return true;
            return false;
        }
        #endregion
    }
}