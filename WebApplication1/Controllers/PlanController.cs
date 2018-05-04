using DayScheduling.Entities.Activity;
using DayScheduling.Entities.Plan;
using DayScheduling.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayScheduling.BLL;
using GoogleMaps.LocationServices;


namespace WebApplication1.Controllers
{
    public class PlanController : Controller
    {
        BLLPlan bllplan = new BLLPlan();
        public ActionResult PlanCriterias()
        {
            return View();
        }

        [HttpPost]
        [DSAuthorize]
        public ActionResult DayByDay(pmPlanCriteria param)
        {
            vmDayByDayPlan Model = bllplan.GetvmDayByDay(param);
            return View(Model);
        }

        public ActionResult PlaceDetail(pmPlaceDetail param)
        {
            vmPlaceDetail model = bllplan.getPlaceDetail(param);
            return View(model);
        }

        public ActionResult DeleteActivity()
        {
            BLLActivity bll = new BLLActivity();
            bll.DeleteActivity(1009);
            return RedirectToAction("DayByDay");
        }
    }
}