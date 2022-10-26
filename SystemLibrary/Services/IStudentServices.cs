using SystemLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.DAL;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Security.Policy;
using SystemLibrary.Models;

namespace SystemLibrary.Services
{
    public interface IStudentServices
    {
        Response RegisterStudent(User model);
        Response UpdateDetails(UpdateStudent model, int StudenId);
        void AssignStatus();
    }
   
}
