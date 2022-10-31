using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Repository;
using RepositoryLibrary.Entities;
using RepositoryLibrary.Models;
using System.Web;

namespace ServicesLibrary.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IUserServices UserServices;
        private readonly IStudentRepository StudentRepository;
        private readonly IUserRepository UserRepository;
        private readonly IValidation Validation;
        public StudentServices(IUserServices userServices, IStudentRepository studentRepository, IUserRepository userRepository,IValidation validation)
        {
            UserServices = userServices;
            StudentRepository = studentRepository;
            UserRepository = userRepository;
            Validation = validation;
        }

        public Response RegisterStudent(User model)
        {
            if (string.IsNullOrEmpty(model.Student.FirstName) || string.IsNullOrEmpty(model.Student.LastName) || string.IsNullOrEmpty(model.Student.NationalID) || model.Student.DateOfBirth.Year >= DateTime.Now.Year)
            {
                string mssg = "Valid values should be entered!";
                return new Response(false, mssg);
            }
            var checkEmailAddress = Validation.IsEmailAvailable(model.EmailAddress);
            if (!checkEmailAddress.Flag)
            {
                return checkEmailAddress;
            }
            var checkPhoneNumber = Validation.IsPhoneNumberAvailable(model.Student.ContactNumber);
            if (!checkPhoneNumber.Flag)
            {
                return checkPhoneNumber;
            }
            var checkNationalID=Validation.IsNationalIDAvailable(model.Student.NationalID);
            if (!checkNationalID.Flag)
            {
                return checkNationalID;
            }
            if (model.Password.Length < 6 || string.IsNullOrEmpty(model.Password))
            {
                string mssg = "Passwords needs to be specified and has to be at least 6 characters long!";
                return new Response(false, mssg);
            }
            
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            model.Password = hashedPassword;
            Response res = StudentRepository.RegisterStudent(model);
            return res;
        }
        public Response UpdateDetails(UpdateStudent model)
        {
            if (string.IsNullOrEmpty(model.GuardianName))
            {
                return new Response(false,"Guardian name is required");
            }
            if (string.IsNullOrEmpty(model.Address.Street))
            {
                return new Response(false, "Street is required");
            }
            if (string.IsNullOrEmpty(model.Address.City))
            {
                return new Response(false, "City is required");
            }
            if (string.IsNullOrEmpty(model.Address.Country))
            {
                return new Response(false, "Country is required");
            }
            if (model.Results.Count == 0)
            {
                return new Response(false, "At least one result is required");
            }
            for (int i = 0; i < model.Results.Count; i++)
            {
                var result = model.Results[i];
                for (int j = i+1; j < model.Results.Count; j++)
                {
                    if (result.Subject.SubjectId == model.Results[j].Subject.SubjectId)
                    {
                        return new Response(false, "Same subjects were entered twice!");
                    }

                }
            }
            var studentId = (int)HttpContext.Current.Session["UserId"];
            return StudentRepository.UpdateDetails(model, studentId);
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
