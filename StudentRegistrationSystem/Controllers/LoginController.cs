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
        public JsonResult AuthenticateUser(string emailAddress, string password)
        {
            Response response = null;
            string url = "";
            try
            {
                response = _userServices.Authenticate(emailAddress, password);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
            }
          
            return Json(response);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}