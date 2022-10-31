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
        private readonly IUserServices UserServices;
        private readonly IValidation Validation;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public UsersController(IUserServices userServices,IValidation validation)
        {
            UserServices = userServices;
            Validation = validation;
        }
        [HttpPost]
        public JsonResult EmailAvailability(string emailAddress)
        {
            try
            {
                return Json(Validation.IsEmailAvailable(emailAddress));
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
                return Json(new Response(false, "An error occured while validating email address"));
            }
        }
        [HttpPost]
        public JsonResult PhoneNumberAvailability(string phoneNumber)
        {
            try
            {
                return Json(Validation.IsPhoneNumberAvailable(phoneNumber));
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
                return Json(new Response(false, "An error occured while validating phone number"));
            }
        }
        [HttpPost]
        public JsonResult NationalIDAvailability(string nationalID)
        {
            try
            {
                return Json(Validation.IsNationalIDAvailable(nationalID));
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
                return Json(new Response(false, "An error occured while validating national identity number"));
            }
        }
    }
}