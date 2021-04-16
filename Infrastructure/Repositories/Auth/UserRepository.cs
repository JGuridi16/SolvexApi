using DataAccess.Entities;
using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;

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

        public User GetAuthenticatedUser(User credentials)
        {
            return UserList.SingleOrDefault(x => x.UserName == credentials.UserName
                        && x.Password == credentials.Password);
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
