using System;
using System.Collections.Generic;

namespace RepositoryLibrary.Entities
{
    public class Student
    {
        public int StudentId { get; private set; }
        public string NationalID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GuardianName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public List<Results> Results { get; set; }
        public Status? StudentStatus { get; set; }
        public Address UserAddress { get; set; }
        public int TotalPoints { get; set; }

        public Student()
        {
            Results = new List<Results>();
        }
        public Student(int studentId)
        {
            StudentId = studentId;
            Results = new List<Results>();
        }

    }
}
