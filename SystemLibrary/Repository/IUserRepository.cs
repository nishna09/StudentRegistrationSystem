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
using SystemLibrary.Entities;
using SystemLibrary.Repository.Database;


namespace SystemLibrary.Repository
{
    public interface IUserRepository
    {
        int Register(User user);
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
        User GetUserByUsername(string userName);
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

        public int Register(User user)
        {
            int UserId=0;

            string query = @"INSERT INTO Users(EmailAddress, UserPassword)";
            query+="VALUES(@EmailAddress, @UserPassword)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmailAddress", user.EmailAddress));
            parameters.Add(new SqlParameter("@UserPassword",user.Password));

            _DBContext.InsertUpdateDelete(query,parameters);
            query = "SELECT UserId FROM Users WHERE EmailAddress=@EmailAddress";
            DataTable result=_DBContext.QueryWithConditions(query, parameters);
            UserId = (int)result.Rows[0]["UserId"];

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
        public User GetUserByUsername(string emailAddress)
        {
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
            return user;
        }

       
        public List<Role> getRoles(int userId)
        {
            List<Role> roles = new List<Role>();
            string query = $"SELECT r.RoleId FROM Roles r" +
                $"INNER JOIN UserRoles ur on (r.RoleId=ur.RoleId)"+
                $"INNER JOIN Users u on (ur.UserId=u.UserId)" +
                $"WHERE u.UserId={userId}";
            //continue to get execute command
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
