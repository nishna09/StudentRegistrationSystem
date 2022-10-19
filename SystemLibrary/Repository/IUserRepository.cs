using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
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
        void Add();
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
        SqlCommand command=null;

        public UserRepository(IDatabaseCommand dBContext)
        {
            _DBContext = dBContext;
            
        }

        public void Add()
        {

        }
        public IEnumerable<User> GetUsers()
        {
            return new List<User>();
        }
        public User GetUserById(int userId)
        {
            return null;
        }
        public User GetUserByUsername(string userName)
        {
            User user = null;
            string query = @"SELECT * FROM Users WHERE UserName=@UserName";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserName", userName));
            DataTable result=_DBContext.QueryWithConditions(query,parameters);
            if (result.Rows.Count > 0)
            {
                DataRow getRow=result.Rows[0];
                user = new User();
                user.UserName = getRow["UserName"].ToString();
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
