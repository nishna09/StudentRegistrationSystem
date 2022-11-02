using RepositoryLibrary.Entities;
using RepositoryLibrary.Models;
using System.Collections.Generic;

namespace ServicesLibrary.Services
{
    public interface IStudentServices
    {
        Response RegisterStudent(User model);
        Response UpdateDetails(UpdateStudent model);
        FormattedStudent ReturnFormattedStudentsWithStatus();
        Response BatchUpdateStatus(FormattedStudent model);
        StudentInfo Get(int? studentId);
        List<StudentSummaryModel> ReturnStudentStatusSummary();
    }
   
}
