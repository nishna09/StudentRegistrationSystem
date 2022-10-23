using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;
using SystemLibrary.Repository.Database;

namespace SystemLibrary.Repository
{
    public interface IStudentRepository
    {
        void RegisterStudent(User user, int id);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly IDatabaseCommand _DBContext;

        public StudentRepository(IDatabaseCommand dBContext)
        {
            _DBContext = dBContext;

        }

        public void RegisterStudent(User user, int id)
        {
            string query = @"INSERT INTO Students(StudentId,NationalID,FirstName, LastName,DateOfBirth,ContactNumber)";
            query += "VALUES(@UserId, @NationalID, @FirstName, @LastName, @DateOfBirth, @ContactNumber)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId",id));
            parameters.Add(new SqlParameter("@NationalID", user.Stud.NationalID));
            parameters.Add(new SqlParameter("@FirstName", user.Stud.FirstName));
            parameters.Add(new SqlParameter("@LastName", user.Stud.LastName));
            parameters.Add(new SqlParameter("@DateOfBirth", user.Stud.DateOfBirth.ToString("yyyy-MM-dd")));
            parameters.Add(new SqlParameter("@ContactNumber", user.Stud.ContactNumber));
            _DBContext.InsertUpdateDelete(query, parameters);
        }
    }
}
