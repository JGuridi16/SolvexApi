using BusinessLayer.Dtos.CityEntity;
using ModelValidator.Interfaces;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces.CityService
{
    public interface ICityService
    {
        IEnumerable<CityDto> GetAll();
        CityDto GetOne(int id);
        IEntityValidator<CityDto> Create(CityDto cityDto);
        IEntityValidator<CityDto> Update(CityDto cityDto);
        IEntityValidator<CityDto> Delete(int id);
    }
}
