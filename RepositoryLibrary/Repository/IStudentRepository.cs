using RepositoryLibrary.Entities;
using RepositoryLibrary.Models;
using System.Collections.Generic;

namespace RepositoryLibrary.Repository
{
    public interface IStudentRepository
    {
        Response RegisterStudent(User user);
        Response UpdateDetails(UpdateStudent model, int userId);
        Student GetStudent(string queryParameter, string parameter, object queryValue);
        List<Student> GetAllStudentsWithResult();
        Response BatchUpdateStatus(FormattedStudent model);
    }

    
}
