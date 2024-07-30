using Perfume.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Perfume.Filters
{
    public class AdminAuthorization : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = SessionConfig.GetUser();
            string Username = user?.tentaikhoan;
            string admin = "Admin";
            bool isInAdminArea = string.Equals(filterContext.RouteData.DataTokens["area"] as string, "Admin", StringComparison.OrdinalIgnoreCase);
            if (Username == null || user.tentaikhoan.ToLower() != admin.ToLower() )
            {
                filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary
            {
                { "controller", "Account" }, // Replace "Account" with your actual controller name
                { "action", "Login" }, // Replace "Login" with your actual action method for login
                { "area", isInAdminArea ? null : "" }
            });
            }
        }
    }
}