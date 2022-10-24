using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLibrary.Entities
{
    public class User
    {
        public int UserId { get; private set; }
        public string EmailAddress { get; set; } 
        //public string Password {
        //    set
        //    {
        //        if (string.IsNullOrEmpty(value))
        //            throw new ArgumentNullException("Must specify password");
        //        else if (value.Length < 6)
        //            throw new Exception("Password must have a minimum of 6 characters");
        //            
        //        Password = value;
        //    }
        //    get { return Password; }
        //}
        
        public string Password { get; set; }
        public Student Stud { get; set; }
        
        public List<Role> Roles { get; set; }

      
        public bool Deleted { get; private set; }
        public User():this(0)
        {

        }
        public User(int id)
        {
            UserId = id;
            Roles = new List<Role>();
        }
        public void SetDeleted(bool deleted)
        {
            Deleted=deleted;
        }
    }
}
