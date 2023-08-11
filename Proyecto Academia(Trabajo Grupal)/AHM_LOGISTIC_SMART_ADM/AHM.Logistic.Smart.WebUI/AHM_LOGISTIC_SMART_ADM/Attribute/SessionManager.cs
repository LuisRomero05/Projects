using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_Libreria.WebUI.Attribute
{
    public class SessionManager : ActionFilterAttribute
    {
        private readonly string _screenName;
        public SessionManager(string screenName)
        {
            _screenName = screenName;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var login = new RouteValueDictionary(new { action = "Login", controller = "User" });
            var notAccess = new RouteValueDictionary(new { action = "NotAccess", controller = "Home" });

            string screens = context.HttpContext.Session.GetString("userpermissions");
            if (string.IsNullOrEmpty(screens))
            {
                context.Result = new RedirectToRouteResult(login);
            }
            else
            {
                if (!screens.Contains(_screenName))
                    context.Result = new RedirectToRouteResult(notAccess);
            }
        }
    }
}
