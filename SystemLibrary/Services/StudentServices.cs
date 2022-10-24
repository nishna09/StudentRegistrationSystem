using SystemLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Repository;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Security.Policy;

namespace SystemLibrary.Services
{
    public interface IStudentServices
    {
        Response RegisterStudent(User model);
        void AssignStatus();
    }
    public class StudentServices:IStudentServices
    {
        private readonly IUserServices _userServices;
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        public StudentServices(IUserServices userServices, IStudentRepository studentRepository, IUserRepository userRepository)
        {
            _userServices = userServices;
            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }

        public Response RegisterStudent(User model)
        {
            var proceed = true;
            string mssg = "";
            if (string.IsNullOrEmpty(model.Stud.FirstName) || string.IsNullOrEmpty(model.Stud.LastName) || string.IsNullOrEmpty(model.Stud.NationalID) || model.Stud.DateOfBirth.Year >= DateTime.Now.Year)
            {
                mssg = "Valid values should be entered!";
                proceed = false;
            }
            if (string.IsNullOrEmpty(model.EmailAddress) || string.IsNullOrEmpty(model.Password))
            {
                mssg = "Email Address and passwords need to be specified!";
                proceed = false;
            }
            
            if (model.Password.Length < 6)
            {
                mssg = "Passwords need to be at least 6 characters long!";
                proceed = false;
            }
            Response res;

            if (proceed){

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                model.Password = hashedPassword;

                res = _studentRepository.RegisterStudent(model);
            }
            else
            {
                res = new Response(proceed, mssg);
            }
            
            return res;
        }

        public void AssignStatus()
        {

        }
        private int CalculateScore()
        {
            return 0;
        }
    }
}
