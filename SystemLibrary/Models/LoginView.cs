using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLibrary.Models
{
    public class LoginView
    {
        public string UserName { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
