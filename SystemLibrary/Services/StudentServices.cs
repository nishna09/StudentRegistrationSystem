using SystemLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Repository;

namespace SystemLibrary.Services
{
    public interface IStudentServices
    {
        void RegisterStudent(User model);
        void AssignStatus();
    }
    public class StudentServices:IStudentServices
    {
        private readonly IUserServices _userServices;
        private readonly IStudentRepository _studentRepository;
        public StudentServices(IUserServices userServices, IStudentRepository studentRepository)
        {
            _userServices = userServices;
            _studentRepository = studentRepository;
        }

        public void RegisterStudent(User model)
        {
            if (string.IsNullOrEmpty(model.Stud.FirstName) || string.IsNullOrEmpty(model.Stud.LastName) || string.IsNullOrEmpty(model.Stud.NationalID) || model.Stud.DateOfBirth.Year >= DateTime.Now.Year)
            {
                throw new ArgumentException("Valid values were not entered!");
            }
           int value= _userServices.Register(model);
            _studentRepository.RegisterStudent(model,value);
        }

        public void AssignStatus()
        {

        }
        private int CalculateScore()
        {
            return 0;
        }
    }
}
