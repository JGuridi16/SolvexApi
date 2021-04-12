using SolvexApi.Interfaces;
using SolvexApi.Models.Auth;
using System.Collections.Generic;

namespace SolvexApi.Contexts
{
    public class UserContext : IUserContext
    {
        private List<User> UserList { get; set; }

        public UserContext()
        {
            LoadUsers();
        }

        public List<User> GetAllUsers()
        {
            return UserList;
        }
        #region Private Methods
        private void LoadUsers()
        {
            UserList = new List<User>()
            {
                new User
                {
                    FullName = "Vaibhav Bhapkar",
                    UserName = "admin",
                    Password = "1234",
                    UserRole = "Admin"
                },
                new User
                {
                    FullName = "Test User",
                    UserName = "user",
                    Password = "1234",
                    UserRole = "User"
                }
            };
        }
        #endregion
    }
}
