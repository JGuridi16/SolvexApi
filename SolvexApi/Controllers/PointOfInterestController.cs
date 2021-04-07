using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolvexApi.Contexts;
using SolvexApi.Entities;
using SolvexApi.Interfaces;
using SolvexApi.Models;
using System;
using System.Collections.Generic;

namespace SolvexApi.Controllers
{
    [ApiController]
    [Route("cities/{cityId}/pointsofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public PointOfInterestController(ILogger<PointOfInterestController> logger, IMailService mailService, ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPointsOfInterest([FromRoute] int cityId)
        {
            try
            {
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} was not found when accesing points of interest.");
                    return NotFound("City not found");
                }

                var listOfPointsOfInterest = _cityInfoRepository.GetAllPointsOfInterestForCity(cityId);

                return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(listOfPointsOfInterest));
            }
            catch
            {
                _logger.LogCritical($"Exception while getting points of interest from city with id {cityId}");
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest([FromRoute] int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId)) return NotFound("City not found");

            var pointOfInterest = _cityInfoRepository.GetOnePointOfInterestForCity(cityId, id);
            if (pointOfInterest == null) return NotFound("Point of interest not found");

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest([FromRoute] int cityId, [FromBody] PointOfInterestDto pointOfInterest)
        {
            bool hasRedFlags = HasRedFlags();
            if (hasRedFlags) return BadRequest(ModelState);

            var cityExists = _cityInfoRepository.CityExists(cityId);
            if (!cityExists) return NotFound();

            var finalPointsOfInterest = _mapper.Map<PointOfInterest>(pointOfInterest);
            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointsOfInterest);
            _cityInfoRepository.Save();

            var createdPointOfInterest = _mapper.Map<PointOfInterestDto>(finalPointsOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new { cityId, Id = createdPointOfInterest.Id, createdPointOfInterest }, createdPointOfInterest);
        }

        [HttpPut]
        public IActionResult UpdatePointOfInterest([FromRoute] int cityId, PointOfInterestDto pointOfInterest)
        {
            bool hasRedFlags = HasRedFlags();
            if (hasRedFlags) return BadRequest(ModelState);

            var cityExists = _cityInfoRepository.CityExists(cityId);
            if (!cityExists) return NotFound();

            var pointOfInterestEntity = _cityInfoRepository.GetOnePointOfInterestForCity(cityId, pointOfInterest.Id);
            if (pointOfInterestEntity == null) return NotFound();

            _mapper.Map(pointOfInterest, pointOfInterestEntity);
            _cityInfoRepository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePointOfInterest([FromRoute] int cityId, [FromRoute] int id, [FromBody] JsonPatchDocument<PointOfInterestDto> patchDoc)
        {
            bool hasRedFlags = HasRedFlags();
            if (hasRedFlags) return BadRequest(ModelState);

            if (!_cityInfoRepository.CityExists(cityId)) return NotFound();

            var pointOfInterestEntity = _cityInfoRepository.GetOnePointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null) return NotFound();

            var pointOfInterestToPatch = _mapper.Map<PointOfInterestDto>(pointOfInterestEntity);
            patchDoc.ApplyTo(pointOfInterestToPatch);

            if (!TryValidateModel(pointOfInterestToPatch)) return BadRequest(ModelState);

            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);
            _cityInfoRepository.Save();
            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public IActionResult DeletePointOfInterest([FromRoute] int cityId, int pointOfInterestId)
        {
            bool hasRedFlags = HasRedFlags();
            if (hasRedFlags) return BadRequest(ModelState);

            var cityExists = _cityInfoRepository.CityExists(cityId);
            if (!cityExists) return NotFound();

            var pointOfInterestEntity = _cityInfoRepository.GetOnePointOfInterestForCity(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null) return NotFound();

            _cityInfoRepository.DeletePointOfInterestForCity(cityId, pointOfInterestEntity);
            _cityInfoRepository.Save();

            _mailService.SendMail("Point of interest deleted", $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted");

            return NoContent();
        }


        private bool HasRedFlags()
        {
            if (!ModelState.IsValid)
            {
                return true;
            }
            return false;
        }
    }
}