using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationSystem.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult HomeAdmin()
        {
            if (this.Session["UserId"] == null)
            {
                RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpGet]
        public ActionResult HomeStudent()
        {
            if (this.Session["UserId"] == null)
            {
                RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}