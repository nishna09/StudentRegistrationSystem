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
    public class ValidationController : Controller
    {
        private readonly IUserServices UserServices;
        private readonly IValidation Validation;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ValidationController(IUserServices userServices,IValidation validation)
        {
            UserServices = userServices;
            Validation = validation;
        }
        [HttpPost]
        public JsonResult EmailAvailability(string emailAddress)
        {
            try
            {
                return Json(Validation.IsEmailAvailable(emailAddress), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
                return Json(new Response(false, "An error occured while validating email address"), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult PhoneNumberAvailability(string phoneNumber)
        {
            try
            {
                return Json(Validation.IsPhoneNumberAvailable(phoneNumber), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
                return Json(new Response(false, "An error occured while validating phone number"), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult NationalIDAvailability(string nationalID)
        {
            try
            {
                return Json(Validation.IsNationalIDAvailable(nationalID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
                return Json(new Response(false, "An error occured while validating national identity number"), JsonRequestBehavior.AllowGet);
            }
        }
    }
}