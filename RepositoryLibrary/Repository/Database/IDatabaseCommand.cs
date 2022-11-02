using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
