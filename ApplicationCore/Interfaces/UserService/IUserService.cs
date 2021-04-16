using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces.UserService
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetAuthenticatedUser(User credentials);
        string GenerateJWTToken(User userInfo);
    }
}
