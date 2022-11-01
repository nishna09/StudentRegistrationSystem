using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Entities;
using System.Net.Mail;
using System.Text.RegularExpressions;
using RepositoryLibrary.Repository;

namespace ServicesLibrary.Services
{
    public class Validation:IValidation
    {
        //The phone number must start with a + followed by number 1-9. The rest can be numbers 0-9. The length should be a min of 7 and max of 14
        private Regex validatePhoneNumberRegex = new Regex("^\\+?[1-9][0-9]{1,3}[0-9]{7,8}$");
        private Regex validateNationalIDRegex = new Regex("^[a-zA-Z0-9]{14,20}$");
        private readonly IStudentRepository StudentRepository;
        private readonly IUserRepository UserRepository;
        public Validation(IStudentRepository studentRepository, IUserRepository userRepository)
        {
            StudentRepository = studentRepository;
            UserRepository = userRepository;
        }

        public Response ValidateEmail(string email)
        {
            bool valid = true;
            var mssg = "";

            if (string.IsNullOrEmpty(email.Trim()))
            {
                valid = false;
                mssg = "Email Address is required!";
            }
            else
            {
                try
                {
                    var emailAddress = new MailAddress(email);
                }
                catch
                {
                    valid = false;
                    mssg = "Email Address is invalid!";
                }
            }
            return new Response(valid,mssg);
        }
        public Response ValidateDateOfBirth(DateTime dateOfBirth)
        {
            bool valid = true;
            var mssg = "";
            var today=DateTime.Now;

            if (today.Year - dateOfBirth.Year < 18)
            {
                valid=false;
                mssg = "Invalid date of birth!";
            }

            return new Response(valid, mssg);
        }
        public Response ValidatePassword(string password)
        {
            bool valid = true;
            var mssg = "";
            
            if (string.IsNullOrEmpty(password.Trim())){
                valid = false;
                mssg = "Password is required!";
            }
            else if (password.Length < 6)
            {
                valid=false;
                mssg = "Password should be at least 6 characters long!";
            }

            return new Response(valid, mssg);
        }
        public Response ValidatePhoneNumber(string phone)
        {
            bool valid = true;
            var mssg = "";
            if (string.IsNullOrEmpty(phone.Trim()))
            {
                valid = false;
                mssg = "Phone number is required!";
            }
            else if (!validatePhoneNumberRegex.IsMatch(phone.Trim()))
            {
                valid = false;
                mssg = "Phone number not valid!";
            }

            return new Response(valid, mssg);
        }
        public Response ValidateNationalID(string nationalID)
        {
            bool valid = true;
            var mssg = "";
            if (string.IsNullOrEmpty(nationalID.Trim()))
            {
                valid = false;
                mssg = "National identity number is required!";
            }
            else if (!validateNationalIDRegex.IsMatch(nationalID.Trim()))
            {
                valid = false;
                mssg = "National identity number is not valid!";
            }

            return new Response(valid, mssg);
        }
        public Response IsPhoneNumberAvailable(string phoneNumber)
        {
            if (!ValidatePhoneNumber(phoneNumber).Flag)
            {
                return ValidatePhoneNumber(phoneNumber);
            }
            var student = StudentRepository.GetStudent("ContactNumber",null, phoneNumber);
            if (student != null)
                return new Response(false, "This phone number is already registered!");
            else
                return new Response(true, "This phone number is available!");
        }
        public Response IsNationalIDAvailable(string nationalID)
        {
            if (!ValidateNationalID(nationalID).Flag)
            {
                return ValidateNationalID(nationalID);
            }
            var student = StudentRepository.GetStudent("NationalID",null, nationalID);
            if (student != null)
                return new Response(false, "This national identity number is already registered!");
            else
                return new Response(true, "This national identity number is available!");
        }
        public Response IsEmailAvailable(string emailAddress)
        {
            if (!ValidateEmail(emailAddress).Flag)
            {
                return ValidateEmail(emailAddress);
            }
            User user = UserRepository.GetUser("EmailAddress", emailAddress);
            if (user != null)
                return new Response(false, "This email address is already registered!");
            else
                return new Response(true, "This email address is available!");
        }
    }
}
