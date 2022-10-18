using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StudentRegistrationSystem;
using SystemLibrary.Entities;
using SystemLibrary.Helper;
using BCrypt.Net;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseConnect db=new DatabaseConnect();
            string userName = "Admin";
            db.Query2($"SELECT * FROM Users WHERE UserName='{userName}'");
            string password = BCrypt.Net.BCrypt.HashPassword("admin");
           // Console.WriteLine(password);
            Console.ReadLine();
        }
    }
}
