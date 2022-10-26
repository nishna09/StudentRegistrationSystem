using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.DAL;
using SystemLibrary.Entities;

namespace SystemLibrary.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserDAL _userRepository;

        public UserServices(IUserDAL userRepository)
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

            User user = _userRepository.GetUserByEmail(model.EmailAddress);
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
            user.Roles = GetRoles(user.UserId);
            return user;

        }
        private List<Role> GetRoles(int userId)
        {
            List<Role> roles = new List<Role>();
            roles = _userRepository.getRoles(userId);
            return roles;
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
