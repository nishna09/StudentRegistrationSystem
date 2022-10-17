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
        public void Update(User user)
        {

        }
        public void Delete(int userId)
        {

        }

        public void getSubject()
        {
            string sqlQuery = "SELECT * FROM Subjects";
            command = new SqlCommand(sqlQuery);
            DataTable dt=_DBContext.Query(command);
            foreach (DataRow row in dt.Rows)
            {
                Debug.WriteLine(row[1].ToString());
            }
        }
    }
}
