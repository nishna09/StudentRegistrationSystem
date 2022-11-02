using RepositoryLibrary.Entities;
using RepositoryLibrary.Repository.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RepositoryLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseCommand DBContext;
        public UserRepository(IDatabaseCommand dBContext)
        {
            DBContext = dBContext;
        }
        public int AddUser(User user, IDatabaseCommand db)
        {
            bool setDb = false;
            if (db == null)
            {
                setDb = true;
                db = DBContext;
                db.OpenDbConnection();
            }
            int userId = 0;
            string query = SQLQueries.AddUserQuery;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmailAddress", user.EmailAddress));
            parameters.Add(new SqlParameter("@UserPassword", user.Password));
            db.InsertUpdateDelete(query, parameters);
            query = SQLQueries.GetLastIdentityInserted;
            DataTable result = db.QueryWithConditions(query, null);
            if (result.Rows.Count > 0)
            {
                userId = Convert.ToInt32(result.Rows[0]["Id"]);
            }
            if (setDb)
                db.CloseDbConnection();
            return userId;
        }
        public User GetUser(string queryParameter, object queryValue)
        {
            DBContext.OpenDbConnection();
            User user = null;
            string query = string.Format($"{SQLQueries.GetUserQuery} WHERE {queryParameter}=@{queryParameter}");
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter($"@{queryParameter}", queryValue));
            DataTable result = DBContext.QueryWithConditions(query, parameters);
            if (result.Rows.Count > 0)
            {
                DataRow getRow = result.Rows[0];
                user = new User((int)getRow["UserId"]);
                user.EmailAddress = getRow["EmailAddress"].ToString();
                user.Password = getRow["UserPassword"].ToString();
                user.SetDeleted((bool)getRow["IsDeleted"]);
            }
            DBContext.CloseDbConnection();
            return user;
        }


    }
}
