using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayScheduling.Data;
using DayScheduling.Entities.Activity;
using DayScheduling.Entities.Plan;
using DayScheduling.Entities.Account;

namespace DayScheduling.BLL
{
    public class BLLActivity
    {
        DALActivity DALAct = new DALActivity();
        DALPlace DALPlace = new DALPlace();

        public void RecordActivity(int ActivityID, string ActivityName, string ActivityType,bool Complete,int PlaceID,int PlanID)
        {
            DALAct.Add(ActivityID, ActivityName, ActivityType, DateTime.Now, DateTime.Now, Convert.ToInt32(Complete), PlaceID, PlanID);
        }

        public void RecordActivities(vmDayByDayPlan model)
        {
            foreach (var item in model.Plan)
            {
                Activity act = new Activity();
                act.ActivityID = int.Parse(item.ActivityID);
                act.ActivityName = "Activity" + model.Plan.IndexOf(item) + AccountUser.User.UserName + AccountUser.Account.AccountID;
                act.ActivityType = item.ActivityPlaceType;
                act.ActivityStartTime = DateTime.Today + item.StartTime;
                act.ActivityFinishTime = DateTime.Today + item.FinishTime;
                act.ActivityComplete = true;
                act.ActivityPlaceID = item.place.PlaceID;
                DALAct.Add(act.ActivityID,act.ActivityName, act.ActivityType, act.ActivityStartTime, act.ActivityFinishTime, Convert.ToInt32(act.ActivityComplete), act.ActivityPlaceID,model.PlanID);
            }
            
        }

        public string DeleteActivity(int ID)
        {
            int res = DALAct.Delete(ID);
            if (res == 1)
            {
                return "Activity is deleted.";
            }
            return "Activity Not Found.";
        }

        public vmPartialActivity GetExistActivity(int ActivityID)
        {
            Activity existActivity = DALAct.Get(ActivityID);
            vmPartialActivity act = new vmPartialActivity();
            act.ActivityPlaceType = existActivity.ActivityType;
            act.place = DALPlace.Get(existActivity.ActivityPlaceID);
            act.StartTime = existActivity.ActivityStartTime.TimeOfDay;
            act.FinishTime = existActivity.ActivityFinishTime.TimeOfDay;
            return act;
        }

        public vmPartialActivity GetActivitytoChange(int BudgetInfo, int NumOfFriends, string PlaceType, int ProvinceID, string Popularity, TimeSpan OldActivityStartTime)
        {
            vmPartialActivity activity = new vmPartialActivity();
            pmPlanCriteria param = new pmPlanCriteria();
            param.ProvinceId = ProvinceID;
            param.style = Popularity;
            param.BudgetInfo = BudgetInfo;

            if (PlaceType == "3" || PlaceType == "11" || PlaceType == "12" || PlaceType == "13" || PlaceType == "15" || PlaceType == "30")
            {
                if (PlaceType == "3" && OldActivityStartTime < new TimeSpan(16, 0, 0))
                    return null;
                activity = getFoodActivity(param, PlaceType, "food");
            }
            else if (PlaceType == "40" || PlaceType == "50" || PlaceType == "60")
            {
                if (((PlaceType == "40" || PlaceType == "50") && OldActivityStartTime < new TimeSpan(16, 0, 0)) || (PlaceType == "60" && (OldActivityStartTime < new TimeSpan(20, 0, 0))))
                    return null;
                activity = getAlcoholActivity(param, PlaceType, "alcohol");
            }
            else if (PlaceType == "10")
            {
                if (OldActivityStartTime > new TimeSpan(10, 0, 0))
                    return null;
                activity = getBreakfastActivity(param, OldActivityStartTime, "food");
            }
            else if (PlaceType == "1")
            {
                activity = getFunActivity(param, "Fun");
            }
            else if (PlaceType == "2")
            {
                activity = getShoppingActivity(param, "Shopping");
            }
            else if (PlaceType == "3")
            {
                activity = getBeachActivity(param, "Beach");
            }
            else if (PlaceType == "4")
            {
                activity = getOutdoorActivity(param, "Outdoor");
            }
            else if (PlaceType == "5")
            {
                activity = getRelaxingActivity(param, "Relaxing");
            }
            else if (PlaceType == "6")
            {
                activity = getCulturelActivity(param, "Culturel");
            }
            return activity;
        }

        public List<vmPartialActivity> GetActivities(int PlanID)
        {
            List<vmPartialActivity> Plan = new List<vmPartialActivity>();
            List<Activity> activityList = DALAct.GetList(PlanID);
            activityList.OrderBy(x => x.ActivityID);
            foreach (var item in activityList)
            {
                vmPartialActivity vmAct = new vmPartialActivity();
                vmAct.ActivityID = item.ActivityID.ToString();
                vmAct.ActivityPlaceType = item.ActivityType;
                vmAct.place = DALPlace.Get(item.ActivityPlaceID);
                vmAct.PlanID = item.PlanID;
                Plan.Add(vmAct);
            }
            return Plan;
        }


        public vmPartialActivity getBreakfastActivity(pmPlanCriteria param, TimeSpan currentTime, string type)
        {
            vmPartialActivity breakfastActivity = new vmPartialActivity();
            Place placeBreakfast = DALPlace.getBreakfastPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            if (placeBreakfast != null)
            {
                breakfastActivity.place = placeBreakfast;
                breakfastActivity.StartTime = currentTime;
                breakfastActivity.FinishTime = breakfastActivity.StartTime.Add(TimeSpan.FromHours(breakfastActivity.place.RecommendedDuration));
                breakfastActivity.ActivityPlaceType = type;
                return breakfastActivity;
            }
            return null;
        }
        public vmPartialActivity getFoodActivity(pmPlanCriteria param, string foodType, string type)
        {
            vmPartialActivity foodActivity = new vmPartialActivity();
            Place foodPlace = new Place();
            if (foodType == "3")
            {
                foodPlace = DALPlace.getRestaurantPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            }
            if (foodType == "11")
            {
                foodPlace = DALPlace.getFastFoodPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            }
            else if (foodType == "12")
            {
                foodPlace = DALPlace.getMeatChickenPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            }
            if (foodType == "13")
            {
                foodPlace = DALPlace.getSeaFoodPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            }
            if (foodType == "15")
            {
                foodPlace = DALPlace.getHomemadePlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            }
            if (foodType == "30")
            {
                foodPlace = DALPlace.getDesertPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            }
            if (foodPlace != null)
            {
                foodActivity.place = foodPlace;
                foodActivity.ActivityPlaceType = type;
                return foodActivity;
            }
            return null;
        }

        public vmPartialActivity getAlcoholActivity(pmPlanCriteria param, string Alcoholtype, string type)
        {
            vmPartialActivity alcoholActivity = new vmPartialActivity();
            Place alcoholPlace = new Place();
            if (Alcoholtype == "40")
            {
                alcoholPlace = DALPlace.getTavernPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            }
            if (Alcoholtype == "50")
            {
                alcoholPlace = DALPlace.getBarPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            }
            else if (Alcoholtype == "60")
            {
                alcoholPlace = DALPlace.getClubPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            }
            if (alcoholPlace != null)
            {
                alcoholActivity.place = alcoholPlace;
                alcoholActivity.ActivityPlaceType = type;
                return alcoholActivity;
            }
            return null;
        }

        public vmPartialActivity getCulturelActivity(pmPlanCriteria param, string type)
        {
            vmPartialActivity culturelActivity = new vmPartialActivity();
            Place placeCulture = DALPlace.getCulturelPlace(param.ProvinceId);
            if (placeCulture != null)
            {
                culturelActivity.place = placeCulture;
                culturelActivity.ActivityPlaceType = type;
                return culturelActivity;
            }
            return null;
        }

        public vmPartialActivity getShoppingActivity(pmPlanCriteria param, string type)
        {
            vmPartialActivity shoppingActivity = new vmPartialActivity();
            Place placeShopping = DALPlace.getShoppingPlace(param.ProvinceId);
            if (placeShopping != null)
            {
                shoppingActivity.place = placeShopping;
                shoppingActivity.ActivityPlaceType = type;
                return shoppingActivity;
            }
            return null;
        }

        public vmPartialActivity getHistoricSitesActivity(pmPlanCriteria param, string type)
        {
            vmPartialActivity historicSitesActivity = new vmPartialActivity();
            Place placeHistoricSite = DALPlace.getHistoricSites(param.ProvinceId);
            if (placeHistoricSite != null)
            {
                historicSitesActivity.place = placeHistoricSite;
                historicSitesActivity.ActivityPlaceType = type;
                return historicSitesActivity;
            }
            return null;
        }
        public vmPartialActivity getFunActivity(pmPlanCriteria param, string type)
        {
            vmPartialActivity FunActivity = new vmPartialActivity();
            Place placeFun = DALPlace.getFunPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            if (placeFun != null)
            {
                FunActivity.place = placeFun;
                FunActivity.ActivityPlaceType = type;
                return FunActivity;
            }
            return null;
        }
        public vmPartialActivity getBeachActivity(pmPlanCriteria param, string type)
        {
            vmPartialActivity BeachActivity = new vmPartialActivity();
            Place placeBeach = DALPlace.getBeachPlace(param.ProvinceId);
            if (placeBeach != null)
            {
                BeachActivity.place = placeBeach;
                BeachActivity.ActivityPlaceType = type;
                return BeachActivity;
            }
            return null;
        }
        public vmPartialActivity getRelaxingActivity(pmPlanCriteria param, string type)
        {
            vmPartialActivity RelaxingActivity = new vmPartialActivity();
            Place placeRelaxing = DALPlace.getRelaxingPlace(param.ProvinceId);
            if (placeRelaxing != null)
            {
                RelaxingActivity.place = placeRelaxing;
                RelaxingActivity.ActivityPlaceType = type;
                return RelaxingActivity;
            }
            return null;
        }
        public vmPartialActivity getOutdoorActivity(pmPlanCriteria param, string type)
        {
            vmPartialActivity OutdoorActivity = new vmPartialActivity();
            Place placeOutdoor = DALPlace.getOutdoorPlace(param.ProvinceId);
            if (placeOutdoor != null)
            {
                OutdoorActivity.place = placeOutdoor;
                OutdoorActivity.ActivityPlaceType = type;
                return OutdoorActivity;
            }
            return null;
        }
    }
}
