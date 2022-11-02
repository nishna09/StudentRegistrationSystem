using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using RepositoryLibrary.Entities;
using RepositoryLibrary.Repository.Database;


namespace RepositoryLibrary.Repository
{
    public interface IUserRepository
    {
        int AddUser(User user, IDatabaseCommand db);
        User GetUser(string queryParameter, object queryValue);
    }

    
}
