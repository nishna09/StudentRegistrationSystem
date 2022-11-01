using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Repository;
using RepositoryLibrary.Entities;
using RepositoryLibrary.Models;
using System.Web;

namespace ServicesLibrary.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IUserServices UserServices;
        private readonly IStudentRepository StudentRepository;
        private readonly IUserRepository UserRepository;
        private readonly IValidation Validation;
        private readonly int? StudentId = null;

        public StudentServices(IUserServices userServices, IStudentRepository studentRepository, IUserRepository userRepository,IValidation validation)
        {
            UserServices = userServices;
            StudentRepository = studentRepository;
            UserRepository = userRepository;
            Validation = validation;
            if (HttpContext.Current.Session["UserId"] != null)
            {
                StudentId = (int)HttpContext.Current.Session["UserId"];
            }
        }
        public Response RegisterStudent(User model)
        {
            if (string.IsNullOrEmpty(model.Student.FirstName) || string.IsNullOrEmpty(model.Student.LastName) || string.IsNullOrEmpty(model.Student.NationalID) || model.Student.DateOfBirth.Year >= DateTime.Now.Year)
            {
                string mssg = "Valid values should be entered!";
                return new Response(false, mssg);
            }
            var checkEmailAddress = Validation.IsEmailAvailable(model.EmailAddress);
            if (!checkEmailAddress.Flag)
            {
                return checkEmailAddress;
            }
            var checkPhoneNumber = Validation.IsPhoneNumberAvailable(model.Student.ContactNumber);
            if (!checkPhoneNumber.Flag)
            {
                return checkPhoneNumber;
            }
            var checkNationalID=Validation.IsNationalIDAvailable(model.Student.NationalID);
            if (!checkNationalID.Flag)
            {
                return checkNationalID;
            }
            if (model.Password.Length < 6 || string.IsNullOrEmpty(model.Password))
            {
                string mssg = "Passwords needs to be specified and has to be at least 6 characters long!";
                return new Response(false, mssg);
            }
            
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            model.Password = hashedPassword;
            Response res = StudentRepository.RegisterStudent(model);
            return res;
        }
        public Response UpdateDetails(UpdateStudent model)
        {
            if (string.IsNullOrEmpty(model.GuardianName))
            {
                return new Response(false,"Guardian name is required");
            }
            if (string.IsNullOrEmpty(model.Address.Street))
            {
                return new Response(false, "Street is required");
            }
            if (string.IsNullOrEmpty(model.Address.City))
            {
                return new Response(false, "City is required");
            }
            if (string.IsNullOrEmpty(model.Address.Country))
            {
                return new Response(false, "Country is required");
            }
            if (model.Results.Count == 0)
            {
                return new Response(false, "At least one result is required");
            }
            var duplicateSubjects = false;
            for (int i = 0; i < model.Results.Count; i++)
            {
                var duplicate=model.Results.Count(result => result.Subject.SubjectId == model.Results[i].Subject.SubjectId);
                if (duplicate > 1)
                {
                    duplicateSubjects=true;
                    break;
                }
            }
            if (duplicateSubjects)
            {
                return new Response(false, "Same subjects were entered twice!");
            }
            var studentId = StudentId;
            return StudentRepository.UpdateDetails(model, (int)studentId);
        }
        public Response CheckIfResultsExists(int? studentId)
        {
            if (studentId == null)
                studentId = StudentId;
            Student student = StudentRepository.GetStudent("st.StudentId", "StudentId", studentId);
            if (student == null)
                return new Response(false, "Error while getting student information");
            else
            {
                if (student.Results == null)
                    return new Response(false, "Results do not exist yet");
                else
                    return new Response(true, "Subjects already exist!");
            }
        }
        public FormattedStudent ReturnFormattedStudentsWithStatus()
        {
            FormattedStudent formattedStudent=new FormattedStudent();
            (List<Student> students, bool isSetStatus) = SortStudentsByPoint();
            if (students != null)
            {
                foreach (var student in students)
                {
                    var results = new List<ResultInfo>();
                    foreach(var studentResult in student.Results)
                    {
                        ResultInfo result=new ResultInfo();
                        result.SubjectName = studentResult.Subject.SubjectName;
                        result.Grade = studentResult.Grade.ToString();
                        results.Add(result);
                    }
                    StudentInfo studentInfo=new StudentInfo();
                    studentInfo.StudentId = student.StudentId;
                    studentInfo.FirstName = student.FirstName;
                    studentInfo.LastName = student.LastName;
                    studentInfo.StudentStatus = student.StudentStatus.ToString();
                    studentInfo.Results = results;
                    studentInfo.TotalPoints = student.TotalPoints;
                    formattedStudent.Students.Add(studentInfo);
                }
                formattedStudent.IsSetStatus= isSetStatus;
            }
            return formattedStudent;
        }
        public (List<Student>, bool) SortStudentsByPoint()
        {
            List<Student> students = GetAllStudentResults();
            if (students == null)
                return (null, false);
            List<Student> studentListWithTotalPoints = new List<Student>();
            foreach(var student in students)
            {
                student.TotalPoints = CalculateScore(student.Results);
                studentListWithTotalPoints.Add(student);
            }
            var orderedStudentListByPoints=studentListWithTotalPoints.OrderByDescending(student => student.TotalPoints).ToList();
            var studentListWithStatus=new List<Student>();
            var isSetStatus = false;
            if (orderedStudentListByPoints[0].StudentStatus == null)
            {
                studentListWithStatus = AssignStatusForAllAstudents(orderedStudentListByPoints);
            }
            else
            {
                studentListWithStatus=orderedStudentListByPoints;
                isSetStatus=true;
            }
            return (studentListWithStatus, isSetStatus);
        }
        private int CalculateScore(List<Results> results)
        {
            int totalPoints = 0;
            foreach(Results result in results)
            {
                totalPoints += (int)result.Grade;
            }
            return totalPoints;
        }
        private List<Student> GetAllStudentResults()
        {
            List<Student> students =StudentRepository.GetAllStudentsWithResult();
            return students;
        }
        private List<Student> AssignStatusForAllAstudents(List<Student> students)
        {
            List<Student> newStudentList = new List<Student>();
            if (students.Count > 15)
            {
                for (int indexStudent = 0; indexStudent < 15; indexStudent++)
                {
                    newStudentList.Add(AssignStatusForEachStudent(students[indexStudent], false));
                }
                for (int indexStudent = 15; indexStudent < students.Count; indexStudent++)
                {
                    newStudentList.Add(AssignStatusForEachStudent(students[indexStudent], true));
                }
            }
            else
            {
                foreach(var student in students)
                {
                    newStudentList.Add(AssignStatusForEachStudent(student, false));
                }
            }
            return newStudentList;
        }
        private Student AssignStatusForEachStudent(Student student, bool waiting)
        {
            if (waiting)
            {
                if (student.TotalPoints >=10)
                    student.StudentStatus = Status.Waiting;
                else
                    student.StudentStatus = Status.Rejected;
            }
            else
            {
                if (student.TotalPoints >=10)
                    student.StudentStatus = Status.Approved;
                else
                    student.StudentStatus = Status.Rejected;
            }
            return student;
        }
    }
}
