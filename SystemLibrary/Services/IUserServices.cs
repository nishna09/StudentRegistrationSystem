using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;
using SystemLibrary.Repository;
using SystemLibrary.Models;
using System.Reflection;

namespace SystemLibrary.Services
{
    public interface IUserServices
    {
        User Authenticate(User model);
        void Logout();
        bool EmailAvailable(string emailName);


    }

    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(User model)
        {
            bool verify = false;
            if (string.IsNullOrEmpty(model.EmailAddress))
            {
                throw new ArgumentNullException("Email Address must be entered!");
            }

            User user=_userRepository.GetUserByEmail(model.EmailAddress);
            if (user != null)
            {
                if (user.Deleted == true)
                {
                    throw new Exception($"Deleted user {user.EmailAddress} trying to login!");
                }

                verify = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            }
            if (!verify)
                return null;

            user.Password = null;
            return user;
            
        }
        public void Logout()
        {

        }
        
        public bool EmailAvailable(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException("Email Address must first be entered!");
            }
            User user = _userRepository.GetUserByEmail(emailAddress);
            if (user != null)
                return false;
            else
                return true;
        }


    }
}
