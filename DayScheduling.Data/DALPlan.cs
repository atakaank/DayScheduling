using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    public class DALPlan
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();
        public int RecordPlan(string PlanName, DateTime PlanDate, int Popularity)
        {
            string query = @"INSERT INTO Plans(PlanID,PlanName,PlanDate,PlanType,PlanPopularity,PlanComplete,PlanRate)
                        Values((SELECT TOP 1 PlanID FROM Plans ORDER BY PlanID DESC)+1,@PlanName,@PlanDate,'Type',@PlanPopularity,1,60)"; //normal sqlserverdaki gibi sql
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@PlanName", PlanName), new SqlParameter("@PlanDate", PlanDate), new SqlParameter("@PlanPopularity", Popularity)); // querye bize gelmiş olan parametreleri ekliyoruz.
            return res;
        }

        public int RecordPlantoHistory(int AccountID,int PlanID)
        {
            string recordPlantoHistory = @"INSERT INTO PlanHistory(AccountID,PlanID) Values(@AccountID,@PlanId)";
            var res = Models.Database.ExecuteSqlCommand(recordPlantoHistory, new SqlParameter("@AccountID",AccountID), new SqlParameter("@PlanID", PlanID));
            return res;
        }

        public int GetLastPlanID()
        {
            string query = @"SELECT TOP 1 PlanID FROM PLANS ORDER BY PlanID DESC"; //cilem.akcay@hotmail.com
            var res = Models.Database.SqlQuery<int>(query);
            int id = res.FirstOrDefault();
            return id;
        }
    }
}
