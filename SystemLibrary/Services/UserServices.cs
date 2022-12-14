using RepositoryLibrary.Entities;
using RepositoryLibrary.Repository;
using System.Collections.Generic;
using System.Web;

namespace ServicesLibrary.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository UserRepository;
        private readonly IValidation Validation;
        private readonly IRoleRepository RoleRepository;

        public UserServices(IUserRepository userRepository, IValidation validation, IRoleRepository roleRepository)
        {
            UserRepository = userRepository;
            Validation = validation;
            RoleRepository = roleRepository;
        }

        public Response Authenticate(string emailAddress, string password)
        {
            string mssg = "";
            if (!Validation.ValidateEmail(emailAddress).Flag)
            {
                return Validation.ValidateEmail(emailAddress);
            }
            if (string.IsNullOrEmpty(password))
            {
                mssg = "Password is required!";
                return new Response(false, mssg);
            }
            User user = UserRepository.GetUser("EmailAddress",emailAddress);
            bool isValid = false;
            string url = "";
            if (user != null)
            {
                if (user.IsDeleted)
                {
                    mssg = $"Deleted user {user.EmailAddress} trying to login!";
                    return new Response(false, mssg);
                }
                isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            }
            if (isValid)
            {
                url = CreateSession(user.UserId);
                mssg = "Authentication successful";
            }
            else
            {
                mssg = "Incorrect credentials";
            }
            return new Response(isValid,mssg,url);
        }
        private string CreateSession(int userId)
        {
            string url = "/Home/HomeStudent";
            List<Role> roles = RoleRepository.GetRoles(userId);
            HttpContext.Current.Session["UserId"] = userId;
            string userRoles = "";
            if (roles != null)
            {
                for (int i = 0; i < roles.Count; i++)
                {
                    userRoles += roles[i].ToString();
                    userRoles += ",";
                    if (roles[i].Equals(Role.Admin))
                    {
                        url = "Home/HomeAdmin";
                    }
                }
            }
            if (!string.IsNullOrEmpty(userRoles))
            {
                HttpContext.Current.Session["Roles"] = userRoles.Remove(userRoles.Length - 1);
            }
            return url;
        }
    }
}
