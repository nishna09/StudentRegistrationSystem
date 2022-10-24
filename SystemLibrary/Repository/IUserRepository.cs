using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemLibrary.Entities;
using SystemLibrary.Repository.Database;


namespace SystemLibrary.Repository
{
    public interface IUserRepository
    {
        int Register(User user, IDatabaseCommand db);
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        List<Role> getRoles(int userId);    
        void Update(User user);
        void Delete(int userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseCommand _DBContext;

        public UserRepository(IDatabaseCommand dBContext)
        {
            _DBContext = dBContext;
            
        }

        public int Register(User user, IDatabaseCommand db)
        {
            bool setDb = false;
            if (db== null)
            {
                setDb = true;
                db = _DBContext;
                db.OpenDbConnection();
            }
            int UserId=0;
            string query = @"INSERT INTO Users(EmailAddress, UserPassword)";
            query+="VALUES(@EmailAddress, @UserPassword)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmailAddress", user.EmailAddress));
            parameters.Add(new SqlParameter("@UserPassword",user.Password));
            db.InsertUpdateDelete(query,parameters);
            query = "SELECT UserId FROM Users WHERE EmailAddress=@EmailAddress";
            DataTable result=db.QueryWithConditions(query, parameters);
            UserId = (int)result.Rows[0]["UserId"];
            if (setDb)
            {
                db.CloseDbConnection();
            }
            return UserId;
        }
        public IEnumerable<User> GetUsers()
        {
            return new List<User>();
        }
        public User GetUserById(int userId)
        {
            return null;
        }
        public User GetUserByEmail(string emailAddress)
        {
            _DBContext.OpenDbConnection();
            User user = null;
            string query = @"SELECT * FROM Users WHERE EmailAddress=@EmailAddress";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmailAddress", emailAddress));
            
            DataTable result=_DBContext.QueryWithConditions(query,parameters);
            if (result.Rows.Count > 0)
            {
                DataRow getRow=result.Rows[0];
                user = new User((int)getRow["UserId"]);
                user.EmailAddress = getRow["EmailAddress"].ToString();
                user.Password = getRow["UserPassword"].ToString();
                user.SetDeleted((bool)getRow["Deleted"]);
            }
            _DBContext.CloseDbConnection();
            return user;
        }

       
        public List<Role> getRoles(int userId)
        {
            List<Role> roles = new List<Role>();
            string query = @"SELECT RoleId FROM UserRoles WHERE UserId=@UserId";

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", userId));

            _DBContext.OpenDbConnection();
            DataTable results= _DBContext.QueryWithConditions(query, parameters);

            if (results.Rows.Count > 0)
            {
                foreach (DataRow row in results.Rows)
                {
                    int roleId = (int)row["RoleId"];
                    roles.Add((Role)roleId);
                }
            }
            _DBContext.CloseDbConnection();
            return roles;
        }
        public void Update(User user)
        {

        }
        public void Delete(int userId)
        {

        }

       
    }
}
