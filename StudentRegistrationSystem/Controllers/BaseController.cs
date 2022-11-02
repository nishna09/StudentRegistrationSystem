using NLog;
using System;
using System.Web.Mvc;

namespace StudentRegistrationSystem.Controllers
{
    public class BaseController : Controller
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public void LogError(Exception exception)
        {
            Logger.Error("Error {err} with inner exception {innerException} occured at {place}", exception.Message, exception.InnerException, exception.Source);
        }
    }
}