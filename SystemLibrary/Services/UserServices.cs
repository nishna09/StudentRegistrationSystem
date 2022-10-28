﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Repository;
using RepositoryLibrary.Entities;
using System.Web;

namespace ServicesLibrary.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Response Authenticate(string emailAddress, string password)
        {
            bool verify = false;
            string mssg = "";
            string url = "/Home/HomeStudent";
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException("Email Address must be entered!");
            }

            User user = _userRepository.GetUser("EmailAddress",emailAddress);
            if (user != null)
            {
                if (user.IsDeleted)
                {
                    throw new Exception($"Deleted user {user.EmailAddress} trying to login!");
                }

                verify = BCrypt.Net.BCrypt.Verify(password, user.Password);
            }
            if (!verify)
                return null;

            user.Roles = GetRoles(user.UserId);
            HttpContext.Current.Session["UserId"]=user.UserId;
            string userRoles = "";
            if (user.Roles != null)
            {
                for (int i = 0; i < user.Roles.Count; i++)
                {
                    userRoles += user.Roles[i].ToString();
                    if (user.Roles[i].Equals(Role.Admin))
                    {
                        url = "Home/HomeAdmin";
                    }
                }
            }
            HttpContext.Current.Session["Roles"] = userRoles;

            return new Response(verify,mssg,url);

        }
        private List<Role> GetRoles(int userId)
        {
            List<Role> roles = new List<Role>();
            roles = _userRepository.getRoles(userId);
            return roles;
        }

        public bool IsEmailAvailable(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException("Email Address must first be entered!");
            }
            User user = _userRepository.GetUser("EmailAddress",emailAddress);
            if (user != null)
                return false;
            else
                return true;
        }
    }
}
