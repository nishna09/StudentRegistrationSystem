using RepositoryLibrary.Entities;
using System.Collections.Generic;

namespace RepositoryLibrary.Models
{
    public class UpdateStudent
    {
        public List<Results> Results { get; set; }
        public Address Address { get; set; } 
        public string GuardianName { get; set; }
        public UpdateStudent()
        {
            Results=new List<Results>();
        }


    }
}
