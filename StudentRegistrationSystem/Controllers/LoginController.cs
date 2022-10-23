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
     
        public ActionResult Index()
        {
            return View();
        }
       
        public JsonResult AuthenticateUser(LoginView model)
        {
            var validUser = false;
            try
            {
                validUser = _userServices.Authenticate(model);
                if (validUser)
                {
                    this.Session["CurrentUser"] = model.UserName;
                    //add role to session
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} with inner exception {ex}",ex.Message, ex.InnerException);
            }
          
            return Json(new { result = validUser, url = Url.Action("actionName", "ControllerName") });
        }
    }
}