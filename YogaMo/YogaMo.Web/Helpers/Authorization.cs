using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Helpers
{
    public class AuthorizationAttribute : TypeFilterAttribute
    {
        public AuthorizationAttribute(bool isAdmin = false, bool isInstructor = false, bool isClient = false) : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { isAdmin, isInstructor, isClient };
        }
    }

    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool admin, bool instructor, bool client)
        {
            _admin = admin;
            _instructor = instructor;
            _client = client;
        }

        private readonly bool _admin;
        private readonly bool _instructor;
        private readonly bool _client;

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            var IsAdministrator = filterContext.HttpContext.GetLoggedInAdministrator();
            var IsInstructor = filterContext.HttpContext.GetLoggedInInstructor();
            var IsClient = filterContext.HttpContext.GetLoggedInClient();

            if (IsAdministrator == null && IsInstructor == null && IsClient == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_message"] = "You are not logged in.";
                }

                filterContext.Result = new RedirectToActionResult("Index", "Login", new { area = "" });
                return;
            }

            _150222Context db = filterContext.HttpContext.RequestServices.GetService(typeof(_150222Context)) as _150222Context;

            // admin can access
            if (_admin && IsAdministrator != null)
            {
                await next();
                filterContext.Result = new RedirectToActionResult("Index", "Home", new { Area = "Admin" });
                return;
            }
            // instructor can access
            if (_instructor && IsInstructor != null)
            {
                await next();
                filterContext.Result = new RedirectToActionResult("Index", "Home", new { Area = "Repairman" });
                return;
            }
            // client can access
            if (_client && IsClient != null)
            {
                await next();
                filterContext.Result = new RedirectToActionResult("Index", "Home", new { Area = "Seller" });
                return;
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.TempData["error_message"] = "You don't have access to this resource.";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Login", new { @area = "" });
        }

        public void OnActionExecuted(ActionResult context) { }
    }
}
