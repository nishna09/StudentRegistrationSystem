using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicesLibrary.Services;
using  RepositoryLibrary.Entities;
using RepositoryLibrary.Models;
using System.Reflection;

namespace StudentRegistrationSystem.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUserServices UserServices;
        public LoginController(IUserServices userServices)
        {
            UserServices = userServices;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AuthenticateUser(string emailAddress, string password)
        {
            Response response = null;
            try
            {
                response = UserServices.Authenticate(emailAddress, password);
            }
            catch (Exception exception)
            {
                LogError(exception);
            }
          
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}