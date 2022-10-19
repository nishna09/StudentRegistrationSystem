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

namespace SystemLibrary.Repository.Database
{
    public interface IDatabaseCommand
    {
        void InsertUpdateDelete(string query, List<SqlParameter> parameters);
        DataTable QueryWithoutConditions(string query);
        DataTable QueryWithConditions(string query, List<SqlParameter> parameters);
    }
    public class DatabaseCommand: IDatabaseCommand
    {
        public SqlConnection conn = null;
        SqlDataAdapter adapter = null;
        private readonly string connetionString = "Data Source=L-PW02X07Y;Initial Catalog=StudentRegistrationSystem;Integrated Security=True";

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private void OpenDbConnection()
        {
            conn = new SqlConnection(connetionString);
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.Open();
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

        public void InsertUpdateDelete(string query, List<SqlParameter> parameters)
        {
            OpenDbConnection();

        }

        public DataTable QueryWithConditions(string query, List<SqlParameter> parameters)
        {
            OpenDbConnection();
            DataTable data = new DataTable();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter => {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                using (SqlDataAdapter sda = new SqlDataAdapter(command))
                {
                    sda.Fill(data);
                }
            }
            CloseDbConnection();
            return data;
        }

        public DataTable QueryWithoutConditions(string query)
        {
            OpenDbConnection();
            DataTable data = new DataTable();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.CommandType = CommandType.Text;
                
                using (SqlDataAdapter sda = new SqlDataAdapter(command))
                {
                    sda.Fill(data);
                }
            }
            CloseDbConnection();
            return data;
        }
    }
}
