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
            if (string.IsNullOrEmpty(model.Student.FirstName) || string.IsNullOrEmpty(model.Student.LastName) || string.IsNullOrEmpty(model.Student.NationalID) || model.Student.DateOfBirth.Year >= DateTime.Now.Year)
            {
                string mssg = "Valid values should be entered!";
                return new Response(false, mssg);
            }
            if (string.IsNullOrEmpty(model.EmailAddress) || string.IsNullOrEmpty(model.Password))
            {
                string mssg = "Email Address and passwords need to be specified!";
                return new Response(false, mssg);
            }

            if (model.Password.Length < 6)
            {
                string mssg = "Passwords need to be at least 6 characters long!";
                return new Response(false, mssg);
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            model.Password = hashedPassword;
            Response res = _studentRepository.RegisterStudent(model);
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
