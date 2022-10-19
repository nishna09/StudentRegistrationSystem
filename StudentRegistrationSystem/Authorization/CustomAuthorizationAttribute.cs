using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentRegistrationSystem.Authorization
{
    public class CustomAuthorizationAttribute: ActionFilterAttribute
    {
        public string Roles { get; set; }
        public string[] AuthorizedRoles { get; set; }
        public CustomAuthorizationAttribute(string Roles)
        {
            this.Roles = Roles;
            AuthorizedRoles = this.Roles.Split(',');
        }
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var dfcontroller = actionContext.Controller as Controller;

            if (dfcontroller.Session["User"] != null)
            {
                var userRoles=dfcontroller.Session["Roles"];
               
                if (!AuthorizedRoles.Contains(userRoles))
                {
                    actionContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Common", action = "Index" }));
                }

            }
            else
            {
                actionContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Common", action = "Index" }));
            }
        }
    }
}