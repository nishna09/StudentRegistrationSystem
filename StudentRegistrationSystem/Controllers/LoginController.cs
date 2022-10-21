using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemLibrary.Services;
using SystemLibrary.Entities;

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
       //public bool Index(){
       //    bool res=_userServices.Login("Admin", "admin1");
       //    return res;
       //}
        public ActionResult Index()
        {
            return View();
        }
    }
}