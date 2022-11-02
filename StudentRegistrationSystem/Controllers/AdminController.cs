using RepositoryLibrary.Entities;
using RepositoryLibrary.Models;
using ServicesLibrary.Services;
using StudentRegistrationSystem.Authorization;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StudentRegistrationSystem.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IStudentServices StudentServices;
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
            catch (Exception exception)
            {
                LogError(exception);
            }
            
            return Json(studentsObj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [CustomAuthorize(Role.Admin)]
        public JsonResult GetStudentSummary()
        {
            List<StudentSummaryModel> summary = null;
            try
            {
                summary = StudentServices.ReturnStudentStatusSummary();

            }
            catch (Exception exception)
            {
                LogError(exception);
            }

            return Json(summary, JsonRequestBehavior.AllowGet);
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
            catch (Exception exception)
            {
                LogError(exception);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [CustomAuthorize(Role.Admin)]
        public ActionResult Summary()
        {
            return View();
        }

    }
}