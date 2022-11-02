using RepositoryLibrary.Entities;
using ServicesLibrary.Services;
using StudentRegistrationSystem.Authorization;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StudentRegistrationSystem.Controllers
{
    public class SubjectController : BaseController
    {
        private readonly IResultServices ResultServices;
        public SubjectController(IResultServices resultServices)
        {

            ResultServices = resultServices;
        }
        [HttpGet]
        [CustomAuthorize]
        public JsonResult GetAllSubjects()
        {
            List<Subject> subjects=new List<Subject>();
            try
            {
                subjects = ResultServices.GetAllSubjects();
            }
            catch (Exception exception)
            {
                LogError(exception);
            }
            return Json(subjects, JsonRequestBehavior.AllowGet);
        }

    }
}