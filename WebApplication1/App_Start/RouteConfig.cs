using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DayScheduling.Entities.Activity;
using JsAction;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.MapRoute(
                name: "isLoginSuccess",
                url: "Account/isLoginSuccess",
                defaults: new { controller = "Account", action = "isLoginSuccess"}
                );

            routes.MapRoute(
                name: "LoginPage",
                url: "Account/LoginPage",
                defaults: new { controller = "Account", action = "LoginPage" }
                );

            routes.MapRoute(
                name: "isSignUpSuccess",
                url: "Account/isSignUpSuccess",
                defaults: new { controller = "Account", action = "isSignUpSuccess" }
                );

            routes.MapRoute(
                name: "SignUp",
                url: "Account/SignUp",
                defaults: new { controller = "Account", action = "SignUp" }
                );

            routes.MapRoute(
                name: "PlanCriterias",
                url: "Plan/PlanCriterias",
                defaults: new { controller = "Plan", action = "PlanCriterias" }
                );

            //routes.MapRoute(
            //    name: "DayByDay",
            //    url: "{controller}/{action}/{Plan}",
            //    defaults: new { controller = "Plan", action = "DayByDay", Plan = new List<vmPartialActivity>()}
            //    );


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
