using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;
using SystemLibrary.Repository.Database;

namespace SystemLibrary.Repository
{
    public interface IResultRepository
    {
        List<Subject> GetAllSubjects();
        List<Grade> GetAllGrades();
        Grade GetGrade(int id);
    }

    public class ResultRepository : IResultRepository
    {
        private readonly IDatabaseCommand _dbContext;
        public ResultRepository(IDatabaseCommand dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Grade> GetAllGrades()
        {
            var grades=new List<Grade>();
            _dbContext.OpenDbConnection();

            string query = "SELECT * FROM Grades";
            DataTable results= _dbContext.QueryWithConditions(query, null);

            if (results.Rows.Count > 0)
            {
                foreach(DataRow row in results.Rows)
                {
                    var grade = new Grade((int)row["GradeId"]);
                    grade.GradeName = (Char)row["GradeName"];
                    grade.Point = (int)row["GradePoint"];
                }
            }

            _dbContext.CloseDbConnection();
            return grades;
        }

        public Grade GetGrade(int id)
        {
            Grade grade=new Grade(id);
            _dbContext.OpenDbConnection();

            string query = @"SELECT * FROM Grades WHERE GradeId=@GradeId";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@GradeId",id));
            DataTable results = _dbContext.QueryWithConditions(query, parameters);

            if (results.Rows.Count > 0)
            {
                DataRow row = results.Rows[0];
                grade.GradeName = (Char)row["GradeName"];
                grade.Point = (int)row["GradePoint"];
            }

            _dbContext.CloseDbConnection();
            return grade;
        }

        public List<Subject> GetAllSubjects()
        {
            var subjects = new List<Subject>();
            _dbContext.OpenDbConnection();

            string query = "SELECT * FROM Subjects";
            DataTable results = _dbContext.QueryWithConditions(query, null);

            if (results.Rows.Count > 0)
            {
                foreach (DataRow row in results.Rows)
                {
                    var subject = new Subject((int)row["SubjectId"]);
                    subject.SubjectName = row["SubjectName"].ToString();
                }
            }

            _dbContext.CloseDbConnection();
            return subjects;
        }
    }
}
