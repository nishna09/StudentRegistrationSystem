using RepositoryLibrary.Entities;
using ServicesLibrary.Services;
using System;
using System.Web.Mvc;

namespace StudentRegistrationSystem.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUserServices UserServices;
        public LoginController(IUserServices userServices)
        {
            UserServices = userServices;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AuthenticateUser(string emailAddress, string password)
        {
            Response response = null;
            try
            {
                response = UserServices.Authenticate(emailAddress, password);
            }
            catch (Exception exception)
            {
                LogError(exception);
            }
          
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}