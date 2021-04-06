using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolvexApi.Contexts;
using SolvexApi.Entities;
using SolvexApi.Interfaces;
using SolvexApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolvexApi.Controllers
{
    [ApiController]
    [Route("cities/{cityId}/pointsofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailService _mailService;

        public PointOfInterestController(ILogger<PointOfInterestController> logger, IMailService mailService, ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                if (_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} was not found when accesing points of interest.");
                    return NotFound();
                }

                var listOfPointsOfInterest = _cityInfoRepository.GetAllPointsOfInterestForCity(cityId);
                var listPointsOfInterestDto = new List<PointOfInterestDto>();

                AttachPointsOfInterestForCity(listOfPointsOfInterest, ref listPointsOfInterestDto);

                return Ok(listPointsOfInterestDto);
            }
            catch
            {
                _logger.LogCritical($"Exception while getting points of interest from city with id {cityId}");
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId)) return NotFound("City not found");

            var pointOfInterest = _cityInfoRepository.GetOnePointOfInterestForCity(cityId, id);
            if (pointOfInterest == null) return NotFound("Point of interest not found");

            var pointOfInterestDto = new PointOfInterestDto()
            {
                Id = pointOfInterest.Id,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            return Ok(pointOfInterestDto);
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestDto pointOfInterest)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdatePointOfInterest(int cityId, PointOfInterestDto pointOfInterest)
        {
            return Ok();
        }

        [HttpDelete("{pointOfInterestId}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            return Ok();
        }

        #region Private Methods
        private void AttachPointsOfInterestForCity(IEnumerable<PointOfInterest> pointOfInterestsFromRepo, ref List<PointOfInterestDto> pointsOfInterestDto)
        {
            foreach (var pointOfInterest in pointOfInterestsFromRepo)
            {
                pointsOfInterestDto.Add(new PointOfInterestDto()
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