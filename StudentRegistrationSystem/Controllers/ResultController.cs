﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemLibrary.Services;
using SystemLibrary.Entities;
using NLog;
using System.Security.Policy;

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
        // GET: Resuly
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllSubjects()
        {
            List<Subject> subjects=new List<Subject>();
            try
            {
                subjects = _resultServices.GetAllSubjects();
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} with inner exception {ex}", ex.Message, ex.InnerException);
            }

            return Json(new { result = subjects });
        }

        [HttpGet]
        public JsonResult GetAllGrades()
        {
            List<Grade> grades=new List<Grade>();
            try
            {
                grades = _resultServices.GetAllGrades();
            }
            catch (Exception ex)
            {
                logger.Error("Error {err} with inner exception {ex}", ex.Message, ex.InnerException);
            }

            return Json(new { result = grades });
        }
    }
}