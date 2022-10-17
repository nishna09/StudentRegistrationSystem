using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemLibrary.Services;

namespace ResgistrationApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public ActionResult Index()
        {
            Debug.WriteLine("Ok");
            logger.Debug("Ok!");
            _userServices.Login("Name","Name");
            return View();
        }
    }
}