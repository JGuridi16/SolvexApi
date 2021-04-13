using BusinessLayer.Dtos.PointOfInterestEntity;
using BusinessLayer.Interfaces.PointOfInterestService;
using ModelValidator.Interfaces;
using System.Collections.Generic;

namespace BusinessLayer.Services.PointOfInterestEntity
{
    public class PointOfInterestService : IPointOfInterestService
    {
        public IEntityValidator<PointOfInterestDto> Create(int cityId, PointOfInterestDto cityDto)
        {
            throw new System.NotImplementedException();
        }

        public IEntityValidator<PointOfInterestDto> Delete(int cityId, int pointOfInterestId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PointOfInterestDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public PointOfInterestDto GetOne(int cityId, int pointOfInterestId)
        {
            throw new System.NotImplementedException();
        }

        public IEntityValidator<PointOfInterestDto> Update(int cityId, PointOfInterestDto cityDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
