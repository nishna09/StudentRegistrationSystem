using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Entities;
using RepositoryLibrary.Repository.Database;
using RepositoryLibrary.Models;
using System.Data.SqlClient;

namespace RepositoryLibrary.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDatabaseCommand _dBContext;
        private readonly IUserRepository _userRepository;

        public StudentRepository(IDatabaseCommand dBContext, IUserRepository userRepository)
        {
            _dBContext = dBContext;
            _userRepository = userRepository;
        }

        public Response RegisterStudent(User user)
        {
            var success = true;
            var mssg = "";
            int insert = 0;
            _dBContext.OpenDbConnection();
            try
            {
                int id = _userRepository.Register(user, _dBContext);

                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = @"INSERT INTO Students(StudentId,NationalID,FirstName, LastName,DateOfBirth,ContactNumber)";
                query += "VALUES(@UserId, @NationalID, @FirstName, @LastName, @DateOfBirth, @ContactNumber)";
                parameters.Add(new SqlParameter("@UserId", id));
                parameters.Add(new SqlParameter("@NationalID", user.Student.NationalID));
                parameters.Add(new SqlParameter("@FirstName", user.Student.FirstName));
                parameters.Add(new SqlParameter("@LastName", user.Student.LastName));
                parameters.Add(new SqlParameter("@DateOfBirth", user.Student.DateOfBirth.ToString("yyyy-MM-dd")));
                parameters.Add(new SqlParameter("@ContactNumber", user.Student.ContactNumber));
                insert=_dBContext.InsertUpdateDelete(query, parameters);

                parameters = new List<SqlParameter>();
                query = @"INSERT INTO UserRoles VALUES(@UserId,@RoleId)";
                parameters.Add(new SqlParameter("@UserId", id));
                parameters.Add(new SqlParameter("@RoleId", (int)Role.Student));
                insert=_dBContext.InsertUpdateDelete(query, parameters);
                _dBContext.Commit();            
            }
            catch
            {
                _dBContext.Rollback();
                success = false;
            }
            mssg = insert > 0 ? "Registration successful" : "Error occured during registration. Please try again";
            return new Response(success, mssg);

        }

        public Response UpdateDetails(UpdateStudent model, int studentId)
        {
            var success = true;
            var mssg = "";
            int update = 0;
            _dBContext.OpenDbConnection();

            _dBContext.OpenDbConnection();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = @"UPDATE Students SET GuardianName=@GuardianName WHERE StudentId=@StudentId";
                parameters.Add(new SqlParameter("@GuardianName", model.GuardianName));
                parameters.Add(new SqlParameter("@StudentId", studentId));

                update=_dBContext.InsertUpdateDelete(query, parameters);

                query = "";
                parameters = new List<SqlParameter>();
                query = @"INSERT INTO Addresses(Street, City, Country, StudentId) ";
                query += @"VALUES(@Street,@City, @Country,@StudentId)";
                parameters.Add(new SqlParameter("@Street", model.Address.Street));
                parameters.Add(new SqlParameter("@City", model.Address.City));
                parameters.Add(new SqlParameter("@Country", model.Address.Country));
                parameters.Add(new SqlParameter("@StudentId", studentId));

                update = _dBContext.InsertUpdateDelete(query, parameters);

                for (int i = 0; i < model.Results.Count; i++)
                {
                    var result = model.Results[i];
                    parameters = new List<SqlParameter>();
                    query = @"INSERT INTO Results(StudenId,SubjectId, GradeId) ";
                    query += @"VALUES(@StudenId,@SubjectId, @GradeId)";
                    parameters.Add(new SqlParameter("@StudentId", studentId));
                    parameters.Add(new SqlParameter("@SubjectId", result.SubjectId));
                    parameters.Add(new SqlParameter("@GradeId", result.Grade));
                    update = _dBContext.InsertUpdateDelete(query, parameters);
                }
                _dBContext.Commit();
                
            }
            catch
            {
                _dBContext.Rollback();
                success = false;
            }

            _dBContext.CloseDbConnection();
            mssg = update > 0 ? "Details added successfully" : "Error while adding details. Please try again!";
            return new Response(success, mssg);
        }



    }
}
