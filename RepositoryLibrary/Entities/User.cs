using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary.Entities
{
    public class User
    {
        public int UserId { get; private set; }
        public string EmailAddress { get; set; } 
        public string Password { get; set; }
        public Student Student { get; set; }
        
        public List<Role> Roles { get; set; }

      
        public bool IsDeleted { get; private set; }
        public User():this(0){}
        public User(int id)
        {
            UserId = id;
            Roles = new List<Role>();
        }
        public void SetDeleted(bool deleted)
        {
            IsDeleted=deleted;
        }
    }
}
