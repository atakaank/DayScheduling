using DayScheduling.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using DayScheduling.BLL;
using DayScheduling.Entities.Activity;
using DayScheduling.Entities.Plan;
using DayScheduling.Entities.Account;
using JsAction;

namespace WebApplication1.Controllers
{

    public class HomeController : Controller
    {
        BLLActivity bllAct = new BLLActivity();
        BLLAccount bllAcc = new BLLAccount();

        public ActionResult Index()
        {
            if (Session["Account"] == null)// Siteye İlk girişte Index sayfasında sessionı açıp değerii sıfır yapıyoruz yani logout durumda.
            {
                Session.Add("Account", "0");
            }
            //else
            //{.
            //    Session["Account"] = "0";
            //}
            return View();
        }
        public ActionResult MainPage()
        {
            return View();
        }
        [Authorize]
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
        public ActionResult PlaceDetail()
        {
            return View();
        }
        public ActionResult UserPage()
        {
            return View();
        }
        public ActionResult GetPlace()
        {
            return View();
        }
    }
    }
