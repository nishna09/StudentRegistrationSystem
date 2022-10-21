using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;
using SystemLibrary.Repository;
using SystemLibrary.Models;

namespace SystemLibrary.Services
{
    public interface IUserServices
    {
        bool Authenticate(LoginView model);
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

        public bool Authenticate(LoginView model)
        {
            bool verify = false;
            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new ArgumentNullException("Username must be entered!");
            }

            User user=_userRepository.GetUserByUsername(model.UserName);
            if (user != null)
            {
                if (user.Deleted == true)
                {
                    throw new Exception("This user has been deleted!");
                }

                verify = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            }
            
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
