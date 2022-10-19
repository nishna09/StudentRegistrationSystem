using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;
using SystemLibrary.Repository;

namespace SystemLibrary.Services
{
    public interface IUserServices
    {
        bool Login(string username, string password);
        void Logout();
        void Register();
        void CheckUserName(string userName);
        
    }

    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("Username must be entered!");
            }

            User user=_userRepository.GetUserByUsername(username);
            if (user == null)
            {
                throw new ArgumentException("Incorrect credentials!");
            }
            if (user.Deleted == true)
            {
                throw new Exception("This user has been deleted!");
            }

            bool verify = BCrypt.Net.BCrypt.Verify(password, user.Password);

            //if (!verify)
            //{
            //    throw new Exception("Incorrect credentials!");
            //}
            


            return verify;
            
        }
        public void Logout()
        {

        }
        public void Register()
        {
            User user = new User();
            
        }
        public void CheckUserName(string userName)
        {

        }

    }
}
