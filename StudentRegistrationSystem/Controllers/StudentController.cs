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
    public class StudentController : Controller
    {
        private readonly IStudentServices _studentServices;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpGet]
        [CustomAuthorize(Role.Student)]
        public ActionResult UpdateProfile()
        {
            return View();
        }
        [HttpPost]
        public JsonResult RegisterStudent(User model)
        {
            Response res = new Response(false, "Unable to register");
            try
            {
                res = _studentServices.RegisterStudent(model);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
            }
            return Json(res);
        }
        [HttpPost]
        [CustomAuthorize(Role.Student, Role.Admin)]
        public JsonResult UpdateStudentDetails(UpdateStudent model)
        {
            Response res = new Response(false, "Unable to update details");
            try
            {
                res = _studentServices.UpdateDetails(model);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
            }
            return Json(res);
        }
    }
}