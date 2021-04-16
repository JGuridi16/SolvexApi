using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetAuthenticatedUser(User credentials);
    }
}
