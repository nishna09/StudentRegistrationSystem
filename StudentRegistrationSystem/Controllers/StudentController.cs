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
        private readonly IStudentServices StudentServices;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public StudentController(IStudentServices studentServices)
        {
            StudentServices = studentServices;
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
                res = StudentServices.RegisterStudent(model);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [CustomAuthorize(Role.Student, Role.Admin)]
        public JsonResult UpdateStudentDetails(UpdateStudent model)
        {
            Response res = new Response(false, "Unable to update details");
            try
            {
                res = StudentServices.UpdateDetails(model);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [CustomAuthorize(Role.Student, Role.Admin)]
        public JsonResult GetStudent()
        {
            StudentInfo student = StudentServices.Get(null);
            return Json(student, JsonRequestBehavior.AllowGet);
        }
    }
}