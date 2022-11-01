using RepositoryLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Security.Policy;
using RepositoryLibrary.Models;

namespace ServicesLibrary.Services
{
    public interface IStudentServices
    {
        Response RegisterStudent(User model);
        Response UpdateDetails(UpdateStudent model);
        (List<Student>, bool) SortStudentsByPoint();
        FormattedStudent ReturnFormattedStudentsWithStatus();
        Response CheckIfResultsExists(int? studentId);
        Response BatchUpdateStatus(FormattedStudent model);
    }
   
}
