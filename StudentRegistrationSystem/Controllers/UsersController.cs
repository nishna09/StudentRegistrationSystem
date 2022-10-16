using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemClassLibrary.Services;

namespace ResgistrationApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}