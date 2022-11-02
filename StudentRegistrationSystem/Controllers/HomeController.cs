using RepositoryLibrary.Entities;
using StudentRegistrationSystem.Authorization;
using System.Web.Mvc;

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

        [CustomAuthorize(Role.Student)]
        [HttpGet]
        public ActionResult HomeStudent()
        {
            return View();
        }
    }
}