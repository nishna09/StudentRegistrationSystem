using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;
using SystemLibrary.Models;

namespace SystemLibrary.DAL
{
    public interface IStudentDAL
    {
        Response RegisterStudent(User user);
        Response UpdateDetails(UpdateStudent model, int userId);
    }

    
}
