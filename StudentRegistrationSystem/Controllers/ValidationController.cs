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
using StudentRegistrationSystem.Controllers;

namespace ResgistrationApplication.Controllers
{
    public class ValidationController : BaseController
    {
        private readonly IValidation Validation;
        public ValidationController(IValidation validation)
        {
            Validation = validation;
        }
        [HttpPost]
        public JsonResult EmailAvailability(string emailAddress)
        {
            try
            {
                return Json(Validation.IsEmailAvailable(emailAddress), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                LogError(exception);
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
            catch (Exception exception)
            {
                LogError(exception);
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
            catch (Exception exception)
            {
                LogError(exception);
                return Json(new Response(false, "An error occured while validating national identity number"), JsonRequestBehavior.AllowGet);
            }
        }
       
    }
}