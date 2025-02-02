﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;
using System.Web.Mvc;
using Perfume.App_Start;
using System.Web.Routing;

namespace Perfume.Filters
{
    public class MyAuthenFilter : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = SessionConfig.GetUser();
            if(user == null)
            {
                filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary
            {
                { "controller", "Account" }, 
                { "action", "Login" } 
            });
            }    
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
           
        }
    }
}