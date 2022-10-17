using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLibrary.Entities
{
    public class User
    {
        public string UserName { get; set; } 
        public string Password {
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Must specify password");
                else if (value.Length < 6)
                    throw new Exception("Password must have a minimum of 6 characters");
                    
                Password = value;
            }
        }

        public Student _student=null;
        public Role _role { get; set; }

        public void CreateStudent()
        {
            _student = new Student();
        }
        
        public bool Deleted { get; private set; }
    }
}
