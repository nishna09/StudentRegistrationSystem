using NLog;
using NLog.Targets;
using RepositoryLibrary.Entities;
using RepositoryLibrary.Models;
using ServicesLibrary.Services;
using StudentRegistrationSystem.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentServices StudentServices;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public AdminController(IStudentServices studentServices)
        {
            StudentServices = studentServices;
        }
        [HttpGet]
        [CustomAuthorize(Role.Admin)]
        public JsonResult GetSortedStudents()
        {
            FormattedStudent studentsObj=null;
            try
            {
                studentsObj = StudentServices.ReturnFormattedStudentsWithStatus();
                
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
            }
            
            return Json(studentsObj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [CustomAuthorize(Role.Admin)]
        public JsonResult BatchUpdateStudentsStatus(FormattedStudent model)
        {
            Response res = null;
            try
            {
                res = StudentServices.BatchUpdateStatus(model);

            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}