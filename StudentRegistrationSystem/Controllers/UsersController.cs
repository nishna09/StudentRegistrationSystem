using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemLibrary.Services;
using SystemLibrary.Models;

namespace ResgistrationApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public ActionResult Register()
        {
            return View();
        }

        public JsonResult RegisterUser()
        {
            return Json(true);
        }

        public JsonResult UserNameAvailability(string userName)
        {
            logger.Error(userName);
            var available = false;
            try
            {
                available = _userServices.UserNameAvailable(userName);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} with inner exception {ex}", ex.Message, ex.InnerException);
            }
            return Json(new { result = available });
        }

    }
}