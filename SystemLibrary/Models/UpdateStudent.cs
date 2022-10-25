using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;

namespace SystemLibrary.Models
{
    public class UpdateStudent
    {
        public List<Results> Results { get; set; }
        public Address Address { get; set; } 
        public string GuardianName { get; set; }


    }
}
