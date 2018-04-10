using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayScheduling.Data;
using DayScheduling.Entities.Activity;

namespace DayScheduling.BLL
{
    public class BLLActivity
    {
        DALActivity DALAct = new DALActivity();
        DALPlace DALPlace = new DALPlace();


        public vmActivitiyEntities GetvmActivities()
        {
            Activity DBactivity = DALAct.Get(0); // controllerdan gelcek.
            List<string> NameCategoryPriceRating = DALPlace.GetPlaceTypeFromPlaceID(DBactivity.ActivityPlaceID);
            vmActivitiyEntities Activities = new vmActivitiyEntities();
            List<vmPartialActivity> partialActivityList = new List<vmPartialActivity>();
            vmPartialActivity activity = new vmPartialActivity();

            activity.ActivityPlaceName = NameCategoryPriceRating[0];
            activity.StartTime = DBactivity.ActivityStartTime;
            activity.FinishTime = DBactivity.ActivityFinishTime;
            activity.ActivityPlaceCategory = NameCategoryPriceRating[1];
            activity.ActivityPlaceRating = "width:"+NameCategoryPriceRating[2]+"%;"; //width:90.0%;
            activity.ActivityPrice = NameCategoryPriceRating[3];
            activity.ActivityPlaceDescrp = NameCategoryPriceRating[4];
            partialActivityList.Add(activity);
            Activities.Activities = partialActivityList; //Activities.Activities initiliaze edilmeli.
            return Activities; 
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
    }
}
