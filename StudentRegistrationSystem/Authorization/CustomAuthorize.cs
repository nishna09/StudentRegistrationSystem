using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Services.Protocols;
using SystemLibrary.Entities;

namespace StudentRegistrationSystem.Authorization
{
    public class CustomAuthorize: ActionFilterAttribute
    {
        private readonly IList<Role> AuthorisedRoles;

        public CustomAuthorize(params Role[] roles)
        {
            AuthorisedRoles = roles ?? new Role[] { };
        }
        //ActionExecutingContext Provides the context for the ActionExecuting method of the ActionFilterAttribute class.
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var controller = actionContext.Controller as Controller;

            if (controller.Session["UserId"] != null)
            {
                bool isValid = false;
                if (!AuthorisedRoles.Any())
                {
                    isValid = true;
                }
                else
                {
                    string userRoles = controller.Session["Roles"].ToString();

                    if (userRoles != null)
                    {
                        string[] arrUserRoles = userRoles.Split(',');

                        for (int i = 0; i < arrUserRoles.Length; i++)
                        {
                            Role role = (Role)Enum.Parse(typeof(Role), arrUserRoles[i]);
                            if (AuthorisedRoles.Contains(role))
                                isValid = true;
                        }

                    }
                }

                if (!isValid)
                {
                    actionContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Unauthorised" }));
                }

            }
            else
            {
                actionContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
        }
    }
}