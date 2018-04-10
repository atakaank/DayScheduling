using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DayScheduling.Data
{
    public class DALActivity
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();
        public Activity Get(int ID)
        {
            List<Activity> actList = Models.Activities.ToList();
            return actList.ElementAt(ID);
        }
        public List<Activity> GetList()
        {
            return Models.Activities.ToList();
        }

        public int Add(string ActivityName,string ActivityType,DateTime StartTime,DateTime FinishTime,int Complete,int ActivityPlaceID) //Veritabanına gitcek kullanıcı tarafından girilecek parametreler
        {
            string query = @"INSERT INTO Activity(AccountID,ActivityName,ActivityType,ActivityStartTime,ActivityFinishTime,ActivityComplete,ActivityPlaceID)
                        Values((SELECT TOP 1 ActivityID FROM Activity ORDER BY ActivityID DESC)+1,@activityName,@activityType,@startTime,@finishTime,@activityComplete,@activityPlaceID)"; //normal sqlserverdaki gibi sql
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@activityName", ActivityName), new SqlParameter("@activityType", ActivityType), new SqlParameter("@startTime", StartTime),
                        new SqlParameter("@finishTime", FinishTime), new SqlParameter("@activityComplete", Complete), new SqlParameter("@activityPlaceID", ActivityPlaceID)); // querye bize gelmiş olan parametreleri ekliyoruz.
            return res;
        }

        public int Update(int ActivityID,string ActivityName, string ActivityType, DateTime StartTime, DateTime FinishTime, int Complete, int ActivityPlaceID)
        {
            string query = @"UPDATE Activity SET ActivityName = @activityName,ActivityType = @activityType,ActivityStartTime=@startTime,ActivityFinishTime=@finishTime
                            ActivityComplete=@complete,ActivityPlaceID=@placeID WHERE ActivityID=@activityID";
            return Models.Database.ExecuteSqlCommand(query,new SqlParameter("@activityID", ActivityID), new SqlParameter("@activityName", ActivityName), new SqlParameter("@activityType", ActivityType),
                    new SqlParameter("@startTime",StartTime), new SqlParameter("@finishTime",FinishTime),new SqlParameter("@complete",Complete), new SqlParameter("@placeID",ActivityPlaceID));
        }


        public int Delete(int ActivityID)
        {
            var query = @"DELETE FROM Activity WHERE ActivityID = @activityID";
            var res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@activityID",ActivityID));//bulamadığında 0 dönüyor. bulduğunda 1 dönüyor.
            return (res);
        }
    }
}
