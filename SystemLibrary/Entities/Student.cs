﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLibrary.Entities
{
    public class Student
    {
        public string NationalID { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string GuardianName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }

        //readonly
        public int MaxSubjects = 3;
        public Dictionary<Subject, Grade> Result { get; set; }
        public Status StudentStatus { get; set; }
        public Address UserAddress;

    }
}
