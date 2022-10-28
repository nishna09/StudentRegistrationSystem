using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationSystem.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Unauthorised()
        {
            return View();
        }
    }
}