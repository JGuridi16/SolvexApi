using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface ICityRepository
    {
        IEnumerable<City> GetAll();
        City GetOne(int cityId);
        City Create(City city);
        City Update(City cityToUpdate);
        City Delete(int id);
    }
}
