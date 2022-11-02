using RepositoryLibrary.Entities;
using RepositoryLibrary.Models;
using ServicesLibrary.Services;
using StudentRegistrationSystem.Authorization;
using System;
using System.Web.Mvc;

namespace StudentRegistrationSystem.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStudentServices StudentServices;
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
            catch (Exception exception)
            {
                LogError(exception);
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
            catch (Exception exception)
            {
                LogError(exception);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [CustomAuthorize(Role.Student, Role.Admin)]
        public JsonResult GetStudent()
        {
            StudentInfo student = null;
            try
            {
                student= StudentServices.Get(null);
            }
            catch (Exception exception)
            {
                LogError(exception);
            }
            return Json(student, JsonRequestBehavior.AllowGet);
        }
    }
}