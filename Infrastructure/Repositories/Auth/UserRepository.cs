using DataAccess.Entities;
using DataAccess.Interfaces;
using System.Collections.Generic;

namespace DataAccess.Repositories.UserEntity
{
    public class UserRepository : IUserRepository
    {
        private List<User> UserList { get; set; }

        public UserRepository()
        {
            LoadUsers();
        }

        public List<User> GetAll()
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
                    FullName = "Linus Torvals",
                    UserName = "linux",
                    Password = "1234",
                    UserRole = "Admin"
                },
                new User
                {
                    FullName = "Bill Gates",
                    UserName = "msft",
                    Password = "1234",
                    UserRole = "User"
                }
            };
        }
        #endregion
    }
}
