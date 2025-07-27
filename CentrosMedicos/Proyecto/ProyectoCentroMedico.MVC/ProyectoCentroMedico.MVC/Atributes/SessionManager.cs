using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Atributes
{
    public class SessionManager : ActionFilterAttribute
    {
        private readonly string _nombrePantalla;
        public SessionManager(string nombrePantalla)
        {
            _nombrePantalla = nombrePantalla;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var login = new RouteValueDictionary(new { Action = "Login", Controller = "Usuarios" });
            var sinAcceso = new RouteValueDictionary(new { Action = "SinAcceso", Controller = "Home" });
            string pantallas = context.HttpContext.Session.GetString("permisosUsuario");
            if (string.IsNullOrEmpty(pantallas))
            {
                context.Result = new RedirectToRouteResult(login);
            }
            else
            {
                if (!pantallas.Contains(_nombrePantalla))
                {
                    context.Result = new RedirectToRouteResult(sinAcceso);
                }
            }
        }
    }
}
