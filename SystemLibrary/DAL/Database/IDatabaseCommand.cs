using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemLibrary.Entities;

namespace SystemLibrary.DAL.Database
{
    public interface IDatabaseCommand
    {
        void OpenDbConnection();
        void CloseDbConnection();
        void Commit();
        void Rollback();
        void InsertUpdateDelete(string query, List<SqlParameter> parameters);
        DataTable QueryWithConditions(string query, List<SqlParameter> parameters);
    }
    public class DatabaseCommand: IDatabaseCommand
    {
        public SqlConnection conn = null;
        private readonly string connetionString = "Data Source=L-PW02X07Y;Initial Catalog=StudentRegistrationSystem;Integrated Security=True";
        private SqlTransaction Transaction;
        public void OpenDbConnection()
        {
            conn = new SqlConnection(connetionString);
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.Open();
                Transaction = conn.BeginTransaction();

            }
            catch (SqlException ex)
            {
                throw ex;
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
            catch(SqlException ex)
            {
                throw ex;
            }
            return data;
        }

       
        public void InsertUpdateDelete(string query, List<SqlParameter> parameters)
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
                    command.ExecuteNonQuery();
                
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
