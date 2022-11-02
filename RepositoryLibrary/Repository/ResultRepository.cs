using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Entities;
using RepositoryLibrary.Repository.Database;

namespace RepositoryLibrary.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly IDatabaseCommand DBContext;
        public ResultRepository(IDatabaseCommand dbContext)
        {
            DBContext = dbContext;
        }
        public List<Subject> GetAllSubjects()
        {
            var subjects = new List<Subject>();
            DBContext.OpenDbConnection();

            string query = SQLQueries.GetSubjects;
            DataTable results = DBContext.QueryWithConditions(query, null);

            if (results.Rows.Count > 0)
            {
                foreach (DataRow row in results.Rows)
                {
                    var subject = new Subject((int)row["SubjectId"]);
                    subject.SubjectName = row["SubjectName"].ToString();
                    subjects.Add(subject);
                }
            }
            DBContext.CloseDbConnection();
            return subjects;
        }
    }
}
