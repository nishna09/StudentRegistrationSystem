using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemLibrary.Services;
using SystemLibrary.Entities;
using System.Net.Mail;

namespace ResgistrationApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IStudentServices _studentServices;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public UsersController(IUserServices userServices,IStudentServices studentServices)
        {
            _userServices = userServices;
            _studentServices = studentServices;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            if (this.Session["UserId"] == null)
            {
                RedirectToAction("Index", "Login");
            }
            //CHECK WHETHER USER IS A STUDENT FIRST
            return View();
        }

        [HttpPost]
        public JsonResult RegisterStudent(User model)
        {
            Response res=new Response();
            try
            {
                res=_studentServices.RegisterStudent(model);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} with inner exception {ex}", ex.Message, ex.InnerException);
            }
            return Json(res);
        }


        [HttpPost]
        public JsonResult EmailAvailability(string emailAddress)
        {
            var available = false;
            try
            {
                available = _userServices.EmailAvailable(emailAddress);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} with inner exception {ex}", ex.Message, ex.InnerException);
            }
            return Json(new { result = available });
        }

    }
}