using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemLibrary.Services;
using SystemLibrary.Entities;
using SystemLibrary.Models;
using System.Reflection;
using SystemLibrary;

namespace StudentRegistrationSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserServices _userServices;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public LoginController(IUserServices userServices)
        {

            _userServices = userServices;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AuthenticateUser(User model)
        {
            User validUser = null;
            string url = "";
            try
            {
                validUser = _userServices.Authenticate(model);
                if (validUser!=null)
                {
                    this.Session["CurrentUser"] = validUser.UserId;
                    this.Session["Roles"] = validUser.Roles;
                    if (validUser.Roles.Contains(Role.Admin))
                    {
                        url = "/Home/HomeAdmin";
                    }
                    else
                    {
                        url = "/Home/HomeStudent";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} with inner exception {ex}",ex.Message, ex.InnerException);
            }
          
            return Json(new { result = validUser, url = url });
        }

        [HttpGet]
        public void Logout()
        {
            this.Session.Clear();
        }
    }
}