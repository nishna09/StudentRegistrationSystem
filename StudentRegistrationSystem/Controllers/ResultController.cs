using System;
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
    public class ResultController : Controller
    {
        private readonly IResultServices _resultServices;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ResultController(IResultServices resultServices)
        {

            _resultServices = resultServices;
        }
     

        [HttpGet]
        [CustomAuthorize]
        public JsonResult GetAllSubjects()
        {
            List<Subject> subjects=new List<Subject>();
            try
            {
                subjects = _resultServices.GetAllSubjects();
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} occured", ex.Message);
            }

            return Json(new { result = subjects });
        }

    }
}