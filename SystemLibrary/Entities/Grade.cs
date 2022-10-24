using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLibrary.Entities
{
    public class Grade
    {
        public int GradeId { get;  private set; }
        public Char GradeName { get; set; }
        public int Point { get; set; }

        public Grade(int gradeId)
        {
            GradeId = gradeId;
        }
    }
}
