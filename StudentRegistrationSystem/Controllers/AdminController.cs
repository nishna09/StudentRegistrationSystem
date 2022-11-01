using NLog;
using RepositoryLibrary.Entities;
using RepositoryLibrary.Models;
using ServicesLibrary.Services;
using StudentRegistrationSystem.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentServices StudentServices;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public AdminController(IStudentServices studentServices)
        {
            StudentServices = studentServices;
        }
        [HttpGet]
        [CustomAuthorize(Role.Admin)]
        public JsonResult GetSortedStudents()
        {
            List<Student> students = new List<Student>();
            try
            {
                students = StudentServices.SortStudentsByPoint();
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
            }
            return Json(students, JsonRequestBehavior.AllowGet);
        }
    }
}