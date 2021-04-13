using BusinessLayer.Dtos.PointOfInterestEntity;
using ModelValidator.Interfaces;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces.PointOfInterestService
{
    public interface IPointOfInterestService
    {
        IEnumerable<PointOfInterestDto> GetAll();
        PointOfInterestDto GetOne(int cityId, int pointOfInterestId);
        IEntityValidator<PointOfInterestDto> Create(int cityId, PointOfInterestDto cityDto);
        IEntityValidator<PointOfInterestDto> Update(int cityId, PointOfInterestDto cityDto);
        IEntityValidator<PointOfInterestDto> Delete(int cityId, int pointOfInterestId);
    }
}
