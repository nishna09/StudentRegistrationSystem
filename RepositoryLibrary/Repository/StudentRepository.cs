using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Entities;
using RepositoryLibrary.Repository.Database;
using RepositoryLibrary.Models;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

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
                    parameters.Add(new SqlParameter("@SubjectId", model.Results[i].Subject.SubjectId));
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
        public Student GetStudent(string queryParameter, string parameter, object queryValue)
        {
            DBContext.OpenDbConnection();
            Student student = null;
            if (parameter == null)
                parameter=queryParameter;
            string query = string.Format($"{SQLQueries.GetStudentQuery} WHERE {queryParameter}=@{parameter}");
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter($"@{parameter}", queryValue));
            DataTable response = DBContext.QueryWithConditions(query, parameters);
            if (response.Rows.Count > 0)
            {
                DataRow row = response.Rows[0];
                student = new Student();
                student.FirstName = row["FirstName"].ToString();
                student.LastName = row["LastName"].ToString();
                student.NationalID= row["NationalID"].ToString();
                student.GuardianName = row["GuardianName"].ToString();
                student.ContactNumber = row["ContactNumber"].ToString();
                student.DateOfBirth = DateTime.Parse(row["DateOfBirth"].ToString());
                try
                {
                    student.StudentStatus = (Status)((int)row["StatusId"]);
                }
                catch
                {
                    student.StudentStatus = null;
                }
                try
                {
                    Address address = new Address();
                    address.Street = row["Street"].ToString();
                    address.City = row["City"].ToString();
                    address.Country = row["Country"].ToString();
                    student.UserAddress = address;
                }
                catch
                {
                    student.UserAddress = null;
                }
                student.Results=SetUserResults(response);
            }
            DBContext.CloseDbConnection();
            return student;
        }
        private List<Results> SetUserResults(DataTable data)
        {
            List<Results> results = null;
            if (data.Rows.Count > 0)
            {
                results = new List<Results>();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    try
                    {
                        Results result = new Results();
                        Subject subject = new Subject((int)data.Rows[i]["SubjectId"]);
                        subject.SubjectName = data.Rows[i]["SubjectName"].ToString();
                        result.Subject = subject;
                        string gradeStr = data.Rows[i]["Grade"].ToString();
                        result.Grade = (Grade)Enum.Parse(typeof(Grade), gradeStr);
                        results.Add(result);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return results;
        }
        public List<Student> GetAllStudentsWithResult()
        {
            List<Student> students = new List<Student>();
            DBContext.OpenDbConnection();
            string query = SQLQueries.GetAllStudentsId;
            DataTable response = DBContext.QueryWithConditions(query, null);
            DBContext.CloseDbConnection();
            if (response.Rows.Count > 0)
            {
                for (int i = 0; i < response.Rows.Count; i++)
                {
                    DataRow row = response.Rows[i];
                    int studentId = (int)row["StudentId"];
                    Student student = GetStudent("st.StudentId", "StudentId", studentId);
                    if (student.Results != null)
                    {
                        students.Add(student);
                    }  
                }
            }
            return students;
        }
    }
}
