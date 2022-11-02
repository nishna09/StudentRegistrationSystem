﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicesLibrary.Services;
using RepositoryLibrary.Entities;
using NLog;
using System.Security.Policy;
using StudentRegistrationSystem.Authorization;

namespace StudentRegistrationSystem.Controllers
{
    public class ResultController : BaseController
    {
        private readonly IResultServices ResultServices;
        public ResultController(IResultServices resultServices)
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