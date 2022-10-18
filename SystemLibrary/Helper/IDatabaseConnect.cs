using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using SystemLibrary.Entities;

namespace SystemLibrary.Helper
{
    public interface IDatabaseConnect
    {
        void NonQuery(string command);
        DataTable Query(string command);
        DataTable Query2(string command);
    }
    public class DatabaseConnect: IDatabaseConnect
    {
        private SqlConnection conn = null;
        SqlDataAdapter adapter = null;
        private static Logger logger = LogManager.GetCurrentClassLogger();
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

        public void NonQuery(string command)
        {
            try
            {
                Open();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message,ex.InnerException,"Non query sql");
            }
            finally
            {
                Close();
            }
        }

        public DataTable Query(string command)
        {
            DataTable data=null;
            try
            {
                Open();
                adapter =new SqlDataAdapter(command, conn);
                data = new DataTable();
                adapter.Fill(data);
               
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex.InnerException,"Query sql");
            }
            finally
            {
                Close ();
            }
            return data;
        }

        public DataTable Query2(string  command)
        {
            DataTable data = null ;
            try
            {

                Open();
                adapter = new SqlDataAdapter(command, conn);
                data = new DataTable();
                adapter.Fill(data);
                if (data.Rows.Count > 0) { 
                    Console.WriteLine("Getting user!");
                DataRow row = data.Rows[0];
                Console.WriteLine(row["UserName"]);
            }
                //foreach (DataRow row in data.Rows)
                //{
                //Console.WriteLine(row[1].ToString());
                //   Console.WriteLine(row[2].ToString());
                //   }
        
         }
         catch (Exception ex)
         {
             logger.Error(ex.Message, ex.InnerException, "Query sql");
             Console.WriteLine(ex.InnerException);
         }
            finally
            {
                Close();
            }
            return data;
        }
    }
}