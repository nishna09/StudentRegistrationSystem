using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.models
{
    public class RegisterUser
    {
        public string UserName { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
