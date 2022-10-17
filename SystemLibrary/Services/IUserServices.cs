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
        void Login(string username, string password);
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

        public void Login(string username, string password)
        {
            _userRepository.getSubject();
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
