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