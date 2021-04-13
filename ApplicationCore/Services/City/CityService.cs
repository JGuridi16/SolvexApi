using AutoMapper;
using BusinessLayer.Dtos.CityEntity;
using BusinessLayer.Interfaces.CityService;
using DataAccess.Entities;
using DataAccess.Interfaces;
using FluentValidation;
using ModelValidator.ConcreteValidators;
using ModelValidator.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services.CityEntity
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CityDto> _validator;

        public CityService(ICityRepository cityRepository, IMapper mapper, IValidator<CityDto> validator)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public IEnumerable<CityDto> GetAll()
        {
            var cities = _cityRepository.GetAll();
            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public dynamic GetOne(int id, bool includePointsOfInterest = false)
        {
            var city = _cityRepository.GetOne(id);

            if (includePointsOfInterest)
            {
                return _mapper.Map<CityDto>(city);
            }

            return _mapper.Map<CityWithoutPointsOfInterestDto>(city);
        }

        public IEntityValidator<CityDto> Create(CityDto cityDto)
        {
            var validationResult = _validator.Validate(cityDto);
            if (!validationResult.IsValid)
                return new EntityValidator<CityDto>
                {
                    Entity = cityDto,
                    IsValid = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                };

            var cityResult = _cityRepository.GetAll()
                .Any(c => c.Id == cityDto.Id);

            if (cityResult)
                return new EntityValidator<CityDto>
                {
                    Entity = cityDto,
                    IsValid = false,
                    Errors = new List<string>
                    {
                        "This city already exist"
                    }
                };

            var cityToCreate = _mapper.Map<City>(cityDto);
            cityToCreate = _cityRepository.Create(cityToCreate);
            cityDto = _mapper.Map<CityDto>(cityToCreate);

            return new EntityValidator<CityDto>
            {
                Entity = cityDto,
                IsValid = true
            };
        }

        public IEntityValidator<CityDto> Update(CityDto cityDto)
        {
            var validationResult = _validator.Validate(cityDto);
            if (!validationResult.IsValid)
                return new EntityValidator<CityDto>
                {
                    Entity = cityDto,
                    IsValid = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                };

            var cityExists = _cityRepository.GetAll()
                .Any(c => c.Id == cityDto.Id);

            if (!cityExists)
                return new EntityValidator<CityDto>
                {
                    Entity = cityDto,
                    IsValid = false,
                    Errors = new List<string>
                    {
                        "This city does not exists"
                    }
                };

            var cityToUpdate = _cityRepository.GetOne(cityDto.Id);
            cityToUpdate = _cityRepository.Update(cityToUpdate);
            cityDto = _mapper.Map<CityDto>(cityToUpdate);

            return new EntityValidator<CityDto>
            {
                Entity = cityDto,
                IsValid = true
            };
        }

        public IEntityValidator<CityDto> Delete(int id)
        {
            var city = _cityRepository.GetOne(id);

            if (city == null)
                return new EntityValidator<CityDto>
                {
                    Entity = _mapper.Map<CityDto>(city),
                    IsValid = false,
                    Errors = new List<string>
                    {
                        "This city does not exists"
                    }
                };

            city = _cityRepository.Delete(id);
            var cityDto = _mapper.Map<CityDto>(city);
            return new EntityValidator<CityDto>
            {
                Entity = cityDto,
                IsValid = true
            };
        }
    }
}
