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
        //public bool Index1()
        //{
        //    LoginView model=new LoginView();
        //    model.UserName = "Admin";
        //    model.Password = "admin22";
        //    var validUser = AuthenticateUser(model);
        //
        //    return validUser;
        //}
        public JsonResult AuthenticateUser(LoginView model)
        {
            var validUser = _userServices.Authenticate(model);
            if (validUser)
            {
                this.Session["CurrentUser"]=model.UserName;
                //add role to session
            }
            return Json(new { result = validUser, url = Url.Action("actionName", "ControllerName") });
        }
    }
}