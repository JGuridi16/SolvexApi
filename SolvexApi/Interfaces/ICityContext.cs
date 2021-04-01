using SolvexApi.Models;
using System.Collections.Generic;

namespace SolvexApi.Interfaces
{
    public interface ICityContext
    {
        List<CityDto> GetAllCities();
        CityDto GetOneCity(int id);
        void InsertCity(CityDto cityDto);
        bool UpdateCity(CityDto cityDto);
        bool DeleteCity(int id);
    }
}