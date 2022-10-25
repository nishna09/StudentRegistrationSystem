using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;
using SystemLibrary.Repository.Database;
using SystemLibrary.Models;

namespace SystemLibrary.Repository
{
    public interface IStudentRepository
    {
        Response RegisterStudent(User user);
        Response UpdateDetails(UpdateStudent model, int userId);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly IDatabaseCommand _DBContext;
        private readonly IUserRepository _userRepository;

        public StudentRepository(IDatabaseCommand dBContext, IUserRepository userRepository)
        {
            _DBContext = dBContext;
            _userRepository = userRepository;
        }

        public Response RegisterStudent(User user)
        {
            var success=true;
            var mssg = "";
            _DBContext.OpenDbConnection();
            try
            {
                int id=_userRepository.Register(user, _DBContext);

                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = @"INSERT INTO Students(StudentId,NationalID,FirstName, LastName,DateOfBirth,ContactNumber)";
                query += "VALUES(@UserId, @NationalID, @FirstName, @LastName, @DateOfBirth, @ContactNumber)";
                parameters.Add(new SqlParameter("@UserId", id));
                parameters.Add(new SqlParameter("@NationalID", user.Stud.NationalID));
                parameters.Add(new SqlParameter("@FirstName", user.Stud.FirstName));
                parameters.Add(new SqlParameter("@LastName", user.Stud.LastName));
                parameters.Add(new SqlParameter("@DateOfBirth", user.Stud.DateOfBirth.ToString("yyyy-MM-dd")));
                parameters.Add(new SqlParameter("@ContactNumber", user.Stud.ContactNumber));
                _DBContext.InsertUpdateDelete(query, parameters);

                query = "";
                parameters = new List<SqlParameter>();
                query = @"INSERT INTO UserRoles ";
                query += @"VALUES(@UserId,@RoleId)";
                parameters.Add(new SqlParameter("@UserId", id));
                parameters.Add(new SqlParameter("@RoleId", (int)Role.Student));
                _DBContext.InsertUpdateDelete(query, parameters);

                _DBContext.Commit();
                mssg = "Registration successful";
            }
            catch
            {
                _DBContext.Rollback();
                success = false;
                mssg = "Error during registration. Please try again!";
            }
            return new Response(success, mssg);

        }

        public Response UpdateDetails(UpdateStudent model, int studentId)
        {
            var success = true;
            var mssg = "";
            _DBContext.OpenDbConnection();

            _DBContext.OpenDbConnection();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = @"UPDATE Students SET GuardianName=@GuardianName WHERE StudentId=@StudentId";
                parameters.Add(new SqlParameter("@GuardianName", model.GuardianName));
                parameters.Add(new SqlParameter("@StudentId", studentId));
                
                _DBContext.InsertUpdateDelete(query, parameters);

                query = "";
                parameters = new List<SqlParameter>();
                query = @"INSERT INTO Addresses(Street, City, Country, StudentId) ";
                query += @"VALUES(@Street,@City, @Country,@StudentId)";
                parameters.Add(new SqlParameter("@Street", model.Address.Street));
                parameters.Add(new SqlParameter("@City",  model.Address.City));
                parameters.Add(new SqlParameter("@Country", model.Address.Country));
                parameters.Add(new SqlParameter("@StudentId", studentId));

                _DBContext.InsertUpdateDelete(query,parameters);

                for (int i = 0; i < model.Results.Count; i++)
                {
                    var result = model.Results[i];
                    query = "";
                    parameters = new List<SqlParameter>();
                    query = @"INSERT INTO Results(StudenId,SubjectId, GradeId) ";
                    query += @"VALUES(@StudenId,@SubjectId, @GradeId)";
                    parameters.Add(new SqlParameter("@StudentId", studentId));
                    parameters.Add(new SqlParameter("@SubjectId", result.SubjectId));
                    parameters.Add(new SqlParameter("@GradeId", result.GradeId));

                    _DBContext.InsertUpdateDelete(query, parameters);

                }

                _DBContext.Commit();
                mssg = "Details added successfully";
            }
            catch
            {
                _DBContext.Rollback();
                success = false;
                mssg = "Error while adding details. Please try again!";
            }

            _DBContext.CloseDbConnection();
            return new Response(success, mssg);
        }



    }
}
