using DayScheduling.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using DayScheduling.BLL;
using DayScheduling.Entities.Activity;
using JsAction;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        BLLActivity bllAct = new BLLActivity();
        BLLAccount bllAcc = new BLLAccount();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DayByDay()
        {
            
            vmActivitiyEntities Model = bllAct.GetvmActivities();
            return View(Model);
        }

        public ActionResult DeleteActivity()
        {
            BLLActivity bll = new BLLActivity();
            bll.DeleteActivity(1009);
            return RedirectToAction("DayByDay");
        }

        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult isLoginSuccess(string NameOrEmail, string password)
        {
            var resSuccess = new { Success = "True", Message = "", TargetUrl = Url.Action("DayByDay","Home")};
            var resFail = new { Success = "False", Message = "Invalid email or password", TargetUrl = "" };
            if (bllAcc.LoginIsSuccess(NameOrEmail, password))
            {
                return Json(resSuccess,JsonRequestBehavior.AllowGet);
            }
            
            return Json(resFail,JsonRequestBehavior.AllowGet);   
        }
        public ActionResult myPlans()
        {
            return View();
        }
        public ActionResult viewPlan()        
            {
                return View();
            }
            public ActionResult Places()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
       }
    }
