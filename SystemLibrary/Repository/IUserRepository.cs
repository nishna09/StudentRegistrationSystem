using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;
using SystemLibrary.Helper;


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
        void getSubject();
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseConnect _DBContext;
        SqlCommand command=null;

        public UserRepository(IDatabaseConnect dBContext)
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
            string query = $"SELECT * FROM Users WHERE UserName='{userName}'";
            DataTable result=_DBContext.Query(query);
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

        public void getSubject()
        {
            string sqlQuery = "SELECT * FROM Subjects";
            DataTable dt=_DBContext.Query(sqlQuery);
            foreach (DataRow row in dt.Rows)
            {
                Debug.WriteLine(row[1].ToString());
            }
        }
    }
}
