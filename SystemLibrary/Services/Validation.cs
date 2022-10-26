using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;
using System.Net.Mail;

namespace SystemLibrary.Services
{
    public class Validation:IValidation
    {
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
    }
}
