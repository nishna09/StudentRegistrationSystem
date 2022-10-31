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
        private readonly IDatabaseCommand DBContext;
        private readonly IUserRepository UserRepository;
        private readonly IRoleRepository RoleRepository;

        public StudentRepository(IDatabaseCommand dBContext, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            DBContext = dBContext;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }
        public Response RegisterStudent(User user)
        {
            int insert = 0;
            DBContext.OpenDbConnection();
            try
            {
                int userId = UserRepository.AddUser(user, DBContext);
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = SQLQueries.AddStudentQuery;
                parameters.Add(new SqlParameter("@UserId", userId));
                parameters.Add(new SqlParameter("@NationalID", user.Student.NationalID));
                parameters.Add(new SqlParameter("@FirstName", user.Student.FirstName));
                parameters.Add(new SqlParameter("@LastName", user.Student.LastName));
                parameters.Add(new SqlParameter("@DateOfBirth", user.Student.DateOfBirth.ToString("yyyy-MM-dd")));
                parameters.Add(new SqlParameter("@ContactNumber", user.Student.ContactNumber));
                insert=DBContext.InsertUpdateDelete(query, parameters);
                insert = RoleRepository.AddRole(Role.Student, userId, DBContext);
                DBContext.Commit();            
            }
            catch
            {
                DBContext.Rollback();
                throw;
            }
            var mssg = insert > 0 ? "Registration successful" : "Error occured during registration. Please try again";
            var success = insert > 0;
            return new Response(success, mssg);

        }

        public Response UpdateDetails(UpdateStudent model, int studentId)
        {
            var success = true;
            int update = 0;
            DBContext.OpenDbConnection();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = SQLQueries.UpdateStudentGuardian;
                parameters.Add(new SqlParameter("@GuardianName", model.GuardianName));
                parameters.Add(new SqlParameter("@StudentId", studentId));
                update=DBContext.InsertUpdateDelete(query, parameters);
                query = SQLQueries.AddAddress;
                parameters.Add(new SqlParameter("@Street", model.Address.Street));
                parameters.Add(new SqlParameter("@City", model.Address.City));
                parameters.Add(new SqlParameter("@Country", model.Address.Country));
                update = DBContext.InsertUpdateDelete(query, parameters);
                for (int i = 0; i < model.Results.Count; i++)
                {
                    parameters = new List<SqlParameter>();
                    query = SQLQueries.AddResult;
                    parameters.Add(new SqlParameter("@StudentId", studentId));
                    parameters.Add(new SqlParameter("@SubjectId", model.Results[i].SubjectId));
                    parameters.Add(new SqlParameter("@Grade", model.Results[i].Grade.ToString()));
                    update = DBContext.InsertUpdateDelete(query, parameters);
                }
                DBContext.Commit();
            }
            catch
            {
                DBContext.Rollback();
                success = false;
                throw;
            }
            DBContext.CloseDbConnection();
            string mssg = update > 0 ? "Details added successfully" : "Error while adding details. Please try again!";
            return new Response(success, mssg);
        }



    }
}
