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
        public int RecordPlan(string PlanName, DateTime PlanDate, int Popularity,int ProvinceID,int AccountID)
        {
            string query = @"INSERT INTO Plans(PlanID,PlanName,PlanDate,PlanType,PlanPopularity,PlanComplete,PlanRate,ProvinceID,AccountID)
                        Values((SELECT TOP 1 PlanID FROM Plans ORDER BY PlanID DESC)+1,@PlanName,@PlanDate,'Type',@PlanPopularity,1,60,@provinceID,@accountID)"; //normal sqlserverdaki gibi sql
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@PlanName", PlanName), new SqlParameter("@PlanDate", PlanDate), new SqlParameter("@PlanPopularity", Popularity), new SqlParameter("@provinceID",ProvinceID), new SqlParameter("@accountID",AccountID)); // querye bize gelmiş olan parametreleri ekliyoruz.
            return res;
        }

        public int RecordPlantoHistory(int AccountID, int PlanID)
        {
            string recordPlantoHistory = @"INSERT INTO PlanHistory(AccountID,PlanID) Values(@AccountID,@PlanId)";
            var res = Models.Database.ExecuteSqlCommand(recordPlantoHistory, new SqlParameter("@AccountID", AccountID), new SqlParameter("@PlanID", PlanID));
            return res;
        }

        public int GetLastPlanID()
        {
            string query = @"SELECT TOP 1 PlanID FROM PLANS ORDER BY PlanID DESC"; //cilem.akcay@hotmail.com
            var res = Models.Database.SqlQuery<int>(query);
            int id = res.FirstOrDefault();
            return id;
        }

        public List<Plan> GetList(int AccountID)
        {
            string query = @"SELECT * FROM Plans WHERE AccountID = @accountID";
            var res = Models.Database.SqlQuery<Plan>(query, new SqlParameter("@accountID",AccountID));
            return res.ToList();
        }

        public void DeletePlan(int PlanID)
        {
            string query = @"DELETE FROM Activity WHERE PlanID = @PlanID";
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@PlanID",PlanID));
            string query2 = @"DELETE FROM Plans WHERE PlanID = @PlanID";
            res = Models.Database.ExecuteSqlCommand(query2, new SqlParameter("@PlanID", PlanID));
        }

    }
}

