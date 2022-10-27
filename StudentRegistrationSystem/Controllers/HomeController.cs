using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentRegistrationSystem.Authorization;
using SystemLibrary.Entities;

namespace StudentRegistrationSystem.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize(Role.Admin)]
        [HttpGet]
        public ActionResult HomeAdmin()
        {
            return View();
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult HomeStudent()
        {
            return View();
        }
    }
}