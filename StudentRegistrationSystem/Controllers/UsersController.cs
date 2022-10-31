using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicesLibrary.Services;
using RepositoryLibrary.Entities;
using System.Net.Mail;
using RepositoryLibrary.Models;
using StudentRegistrationSystem.Authorization;

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
         [HttpPost]
        public JsonResult EmailAvailability(string emailAddress)
        {
            try
            {
                return Json(_userServices.IsEmailAvailable(emailAddress));
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
                return Json(new Response(false, "An error occured while validating email address"));
            }
        }

    }
}