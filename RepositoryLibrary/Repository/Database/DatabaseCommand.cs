using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RepositoryLibrary.Repository.Database
{
    public class DatabaseCommand : IDatabaseCommand
    {
        private SqlConnection conn = null;
        private readonly string connectionString = @ConfigurationManager.AppSettings["ConnectionString"];
        private SqlTransaction Transaction;
        public void OpenDbConnection()
        {
            conn = new SqlConnection(connectionString);
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                Transaction = conn.BeginTransaction();
            }
            catch
            {
                throw;
            }

        }
        public void CloseDbConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public void Commit()
        {
            Transaction.Commit();
        }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public DataTable QueryWithConditions(string query, List<SqlParameter> parameters)
        {
            DataTable data = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Transaction = Transaction;
                    if (parameters != null)
                    {
                        parameters.ForEach(parameter =>
                        {
                            command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                        });
                    }
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        sda.Fill(data);
                    }
                }
            }
            catch
            {
                throw;
            }
            return data;
        }
        public int InsertUpdateDelete(string query, List<SqlParameter> parameters)
        {
            DataTable data = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Transaction = Transaction;
                    if (parameters != null)
                    {
                        parameters.ForEach(parameter =>
                        {
                            command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                        });
                    }
                    return command.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
