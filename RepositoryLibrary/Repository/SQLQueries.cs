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
        public const string GetLastIdentityInserted = "SELECT @@IDENTITY as Id";
        public const string AddStudentQuery = @"INSERT INTO Students(StudentId,NationalID,FirstName, LastName,DateOfBirth,ContactNumber) VALUES(@UserId, @NationalID, @FirstName, @LastName, @DateOfBirth, @ContactNumber)";
        public const string AddUserRoleQuery = @"INSERT INTO UserRoles VALUES(@UserId,@RoleId)";
        public const string GetSubjects = "SELECT SubjectId, SubjectName FROM Subjects";
        public const string GetUserRoles = @"SELECT RoleId FROM UserRoles WHERE UserId=@UserId";
        public const string UpdateStudentGuardian = @"UPDATE Students SET GuardianName=@GuardianName WHERE StudentId=@StudentId";
        public const string AddAddress = @"INSERT INTO Addresses(Street, City, Country, StudentId) VALUES(@Street,@City, @Country,@StudentId)";
        public const string AddResult = @"INSERT INTO Results(StudentId,SubjectId, Grade) VALUES(@StudentId,@SubjectId, @Grade)";
        public const string GetAllStudentsId = @"SELECT StudentId FROM Students";
        public const string GetStudentQuery = @"SELECT NationalID, FirstName, LastName, GuardianName, DateOfBirth, StatusId, ContactNumber,s.SubjectId, SubjectName, Grade, street, City, Country
                                                from Students st
                                                left join Addresses a on st.StudentId=a.StudentId
                                                left join Results r on st.StudentId=r.StudentId
                                                left join Subjects s on r.SubjectId=s.SubjectId";
    }
}
