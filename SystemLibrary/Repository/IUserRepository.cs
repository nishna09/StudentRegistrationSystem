using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;

namespace SystemLibrary.Repository
{
    public interface IUserRepository
    {
        void Add();
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
        void Update(User user);
        void Delete(int userId);
    }

    public class UserRepository : IUserRepository
    {
        public void Add()
        {

        }
        public IEnumerable<User> GetUsers()
        {
            return new List<User>();
        }
        public User GetUserById(int userId)
        {
            return null;
        }
        public void Update(User user)
        {

        }
        public void Delete(int userId)
        {

        }
    }
}
