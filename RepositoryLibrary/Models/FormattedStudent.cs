using RepositoryLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary.Models
{
    public class FormattedStudent
    {
        public bool IsSetStatus { get; set; }
        public List<StudentInfo> Students { get; set; }
        public FormattedStudent()
        {
            IsSetStatus=false;
            Students=new List<StudentInfo>();
        }
    }
    public class StudentInfo
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentStatus { get; set; }
        public int TotalPoints { get; set; }
        public List<ResultInfo> Results { get; set; }
        public StudentInfo()
        {
            Results=new List<ResultInfo>();
        }
    }
    public class ResultInfo
    {
        public string SubjectName { get; set; }
        public string Grade { get; set; }
    }
}
