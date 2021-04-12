using SolvexApi.Models.Auth;
using System.Collections.Generic;

namespace SolvexApi.Interfaces
{
    public interface IUserContext
    {
        List<User> GetAllUsers();
    }
}
