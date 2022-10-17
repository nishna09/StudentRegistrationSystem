using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;

namespace SystemLibrary.Helper
{
    public interface IDatabaseConnect
    {
        void NonQuery(SqlCommand command);
        DataTable Query(SqlCommand command);
    }
    public class DatabaseConnect: IDatabaseConnect
    {
        private SqlConnection conn = null;
        private void Open()
        {
            string connetionString = "Data Source=L-PW02X07Y;Initial Catalog=StudentRegistrationSystem;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();
            
        }

        private void Close()
        {
           conn.Close();
        }

        public void NonQuery(SqlCommand command)
        {
            try
            {
                Open();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Close();
            }
        }

        public DataTable Query(SqlCommand command)
        {
            DataTable data = null;
            try
            {
                Open();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Close ();
            }
            return data;
        }
    }
}