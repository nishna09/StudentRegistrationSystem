using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary.Repository
{
    public class SQLQueries
    {
        public const string GetUserQuery = @"SELECT UserId, EmailAddress, UserPassword, IsDeleted FROM Users ";
        public const string AddUserQuery = @"INSERT INTO Users(EmailAddress, UserPassword) VALUES(@EmailAddress, @UserPassword)";
        public const string GetLastIdentityInserted = "SELECT @@IDENTITY";
        public const string AddStudentQuery = @"INSERT INTO Students(StudentId,NationalID,FirstName, LastName,DateOfBirth,ContactNumber) VALUES(@UserId, @NationalID, @FirstName, @LastName, @DateOfBirth, @ContactNumber)";
        public const string AddUserRoleQuery = @"INSERT INTO UserRoles VALUES(@UserId,@RoleId)";
    }
}
