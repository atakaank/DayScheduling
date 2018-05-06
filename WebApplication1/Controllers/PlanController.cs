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
        BLLActivity bllact = new BLLActivity();
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

        public ActionResult GetPlanFromHistory(int PlanID)
        {
            TempData["ExistPlan"] = bllplan.GetExistPlan(PlanID);
            var res = new { TargetUrl = Url.Action("GetPlan", "Plan") };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPlan()
        {
            vmDayByDayPlan Model = new vmDayByDayPlan();
            if (TempData.Count != 0)
            {
                var model = TempData["ExistPlan"] as vmDayByDayPlan;
                return View(model);
            }
            return View(Model);
        }
        public ActionResult DeletePlan(int PlanID, bool userpage)
        {
            bllplan.DeletePlan(PlanID);
            var resUserPage = new { TargetUrl = Url.Action("UserPage", "Home") };
            var resMyPlans = new { TargetUrl = Url.Action("myPlans", "Plan") };
            if (userpage)
            {
                return Json(resUserPage, JsonRequestBehavior.AllowGet);
            }
            return Json(resMyPlans, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteActivity(int ID,int PlanID)
        {
            bllact.DeleteActivity(ID);
            TempData["ExistPlan"] = bllplan.GetExistPlan(PlanID);
            var res = new { TargetUrl = Url.Action("GetPlan", "Plan") };
            return Json(res,JsonRequestBehavior.AllowGet);
        }

        public ActionResult PlaceDetail(pmPlaceDetail param)
        {
            vmPlaceDetail model = bllplan.getPlaceDetail(param);
            return View(model);
        }

        [HttpPost]
        public ActionResult GetExistActivity(int ActivityID)
        {
            vmPartialActivity activity =  bllact.GetExistActivity(ActivityID);
            var res = new {PlaceID = activity.place.PlaceID, PlaceName = activity.place.PlaceName, PlaceRate = activity.place.PlaceRate, Activitytype = activity.ActivityPlaceType };
            return Json(res,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeActivity(int ActivityID,int BudgetInfo, int NumOfFriends, string PlaceType, int ProvinceID, string Popularity)
        {
            //bllact.DeleteActivity(int.Parse(param.updatePlaceID));
            vmPartialActivity existAct =  bllact.GetExistActivity(ActivityID);
            vmPartialActivity newAct =  bllact.GetActivitytoChange(BudgetInfo,NumOfFriends,PlaceType,ProvinceID,Popularity,existAct.StartTime);
            var res = new {activityID= newAct.ActivityID, PlaceID = newAct.place.PlaceID, PlaceName = newAct.place.PlaceName, PlaceRate = newAct.place.PlaceRate, Activitytype = newAct.ActivityPlaceType };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateActivity(pmUpdateActivity param)
        {
            bllact.DeleteActivity(param.updateActivityID);
            bllact.RecordActivity(param.updateActivityID, param.updateActivityID + "Activity" + AccountUser.Account.AccountID, param.updateActivityTypeSave, true,param.updatePlaceIDSave, param.updatePlanIDSave);
            TempData["ExistPlan"] =  bllplan.GetExistPlan(param.updatePlanIDSave);
            return RedirectToAction("GetPlan","Plan");
        }

        [DSAuthorize]
        public ActionResult myPlans()
        {
            vmMyPlans model = new vmMyPlans();
            model.PlanBlockList = bllplan.GetPlanBlockList(AccountUser.Account.AccountID, false);
            return View(model);
        }

    }
}