using AutoMapper;
using BusinessLayer.Dtos.PointOfInterestEntity;
using DataAccess.Interfaces;
using MailService.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace SolvexApi.Controllers
{
    [ApiController]
    [Route("cities/{cityId}/pointsofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly IPointOfInterestRepository _pointOfInterestRepository;
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public PointOfInterestController(ILogger<PointOfInterestController> logger, IPointOfInterestRepository pointOfInterestRepository)
        {
            _pointOfInterestRepository = pointOfInterestRepository ?? throw new ArgumentNullException(nameof(pointOfInterestRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult GetPointsOfInterest([FromRoute] int cityId)
        {
            try
            {
                return Ok(_pointOfInterestRepository.GetAll(cityId));
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
            var pointOfInterest = _pointOfInterestRepository.GetOne(cityId, id);
            if (pointOfInterest == null) return NotFound();
            return Ok(pointOfInterest);
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest([FromRoute] int cityId, [FromBody] PointOfInterestDto pointOfInterest)
        {
            bool hasRedFlags = HasRedFlags();
            if (hasRedFlags) return BadRequest(ModelState);

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdatePointOfInterest([FromRoute] int cityId, PointOfInterestDto pointOfInterest)
        {
            bool hasRedFlags = HasRedFlags();
            if (hasRedFlags) return BadRequest(ModelState);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePointOfInterest([FromRoute] int cityId, [FromRoute] int id, [FromBody] JsonPatchDocument<PointOfInterestDto> patchDoc)
        {
            bool hasRedFlags = HasRedFlags();
            if (hasRedFlags) return BadRequest(ModelState);

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public IActionResult DeletePointOfInterest([FromRoute] int cityId, int pointOfInterestId)
        {
            bool hasRedFlags = HasRedFlags();
            if (hasRedFlags) return BadRequest(ModelState);

            return NoContent();
        }

        #region Private Methods
        private bool HasRedFlags()
        {
            if (!ModelState.IsValid)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}