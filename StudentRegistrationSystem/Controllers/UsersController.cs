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
using SystemLibrary.Models;
using StudentRegistrationSystem.Authorization;

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
        [CustomAuthorize(Role.Student)]
        public ActionResult UpdateProfile()
        {
            return View();
        }

        [HttpPost]
        [CustomAuthorize(Role.Student, Role.Admin)]
        public JsonResult RegisterStudent(User model)
        {
            Response res=new Response();
            try
            {
                res=_studentServices.RegisterStudent(model);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
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
                logger.Error("Error {err} occured", ex.Message);
            }
            return Json(new { result = available });
        }

        [HttpPost]
        [CustomAuthorize(Role.Student, Role.Admin)]
        public JsonResult UpdateStudentDetails(UpdateStudent model)
        {
            Response res = new Response(false,"Unable to update details");
            if (this.Session["UserId"] != null)
            {
                try
                {
                    res = _studentServices.UpdateDetails(model, (int)this.Session["UserId"]);
                }
                catch (Exception ex)
                {
                    logger.Error("Error {err} occured", ex.Message);
                }
            }
            
            return Json(res);
        }

    }
}