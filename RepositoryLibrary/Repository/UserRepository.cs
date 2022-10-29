using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Repository.Database;
using RepositoryLibrary.Entities;

namespace RepositoryLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseCommand _dBContext;
        public UserRepository(IDatabaseCommand dBContext)
        {
            _dBContext = dBContext;
        }
        public int AddUser(User user, IDatabaseCommand db)
        {
            bool setDb = false;
            if (db == null)
            {
                setDb = true;
                db = _dBContext;
                db.OpenDbConnection();
            }
            int UserId = 0;
            string query = SQLQueries.AddUserQuery;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmailAddress", user.EmailAddress));
            parameters.Add(new SqlParameter("@UserPassword", user.Password));
            db.InsertUpdateDelete(query, parameters);
            query = SQLQueries.GetLastIdentityInserted;
            DataTable result = db.QueryWithConditions(query, null);
            UserId = (int)result.Rows[0][0];
            if (setDb)
                db.CloseDbConnection();
            return UserId;
        }
        public IEnumerable<User> GetUsers()
        {
            return new List<User>();
        }
        public User GetUser(string queryParameter, object queryValue)
        {
            _dBContext.OpenDbConnection();
            User user = null;
            string query = string.Format($"{SQLQueries.GetUserQuery} WHERE {queryParameter}=@{queryParameter}");
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmailAddress", queryValue));
            DataTable result = _dBContext.QueryWithConditions(query, parameters);
            if (result.Rows.Count > 0)
            {
                DataRow getRow = result.Rows[0];
                user = new User((int)getRow["UserId"]);
                user.EmailAddress = getRow["EmailAddress"].ToString();
                user.Password = getRow["UserPassword"].ToString();
                user.SetDeleted((bool)getRow["IsDeleted"]);
            }
            _dBContext.CloseDbConnection();
            return user;
        }
        public bool Update(User user)
        {
            return false;
        }
        public bool Delete(int userId)
        {
            return false;
        }


    }
}
