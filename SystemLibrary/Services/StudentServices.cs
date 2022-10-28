using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Repository;
using RepositoryLibrary.Entities;
using RepositoryLibrary.Models;

namespace ServicesLibrary.Services
{
    public class StudentServices : IStudentServices
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
            if (string.IsNullOrEmpty(model.Student.FirstName) || string.IsNullOrEmpty(model.Student.LastName) || string.IsNullOrEmpty(model.Student.NationalID) || model.Student.DateOfBirth.Year >= DateTime.Now.Year)
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

            if (proceed)
            {

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

        public Response UpdateDetails(UpdateStudent model, int StudenId)
        {
            return _studentRepository.UpdateDetails(model, StudenId);
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
