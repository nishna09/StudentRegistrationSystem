using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;
using SystemLibrary.DAL.Database;

namespace SystemLibrary.DAL
{
    public class ResultDAL : IResultDAL
    {
        private readonly IDatabaseCommand _dbContext;
        public ResultDAL(IDatabaseCommand dbContext)
        {
            _dbContext = dbContext;
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
                    subjects.Add(subject);
                }
            }

            _dbContext.CloseDbConnection();
            return subjects;
        }
    }
}
