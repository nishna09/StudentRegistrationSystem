using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Entities;
using RepositoryLibrary.Models;

namespace RepositoryLibrary.Repository
{
    public interface IStudentRepository
    {
        Response RegisterStudent(User user);
        Response UpdateDetails(UpdateStudent model, int userId);
        Student GetStudent(string queryParameter, object queryValue);
    }

    
}
