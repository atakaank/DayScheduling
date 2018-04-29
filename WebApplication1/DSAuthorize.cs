using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1
{
    public class DSAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Account"].ToString() == "0")
            {
                filterContext.HttpContext.Response.Redirect("/Account/LoginPage");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}