using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Entities;

namespace RepositoryLibrary.Repository.Database
{
    public interface IDatabaseCommand
    {
        void OpenDbConnection();
        void CloseDbConnection();
        void Commit();
        void Rollback();
        int InsertUpdateDelete(string query, List<SqlParameter> parameters);
        DataTable QueryWithConditions(string query, List<SqlParameter> parameters);
    }
   
}
