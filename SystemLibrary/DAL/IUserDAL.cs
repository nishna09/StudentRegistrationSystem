using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using SystemLibrary.Entities;
using SystemLibrary.DAL.Database;


namespace SystemLibrary.DAL
{
    public interface IUserDAL
    {
        int Register(User user, IDatabaseCommand db);
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        List<Role> getRoles(int userId);    
        void Update(User user);
        void Delete(int userId);
    }

    
}
