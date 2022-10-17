using SystemLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLibrary.Services
{
    public interface IStudentServices
    {
        void RegisterStudent();
        void AssignStatus();
    }
    public class StudentServices:IStudentServices
    {
        private readonly IUserServices _userServices;
        public StudentServices(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public void RegisterStudent()
        {

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
