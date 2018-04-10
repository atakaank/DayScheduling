using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    class DALPlanCriteriasID
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();
        public PlanCriteria Get(int ID)
        {
            string query = @"SELECT * FROM PlanCriteria PC, WHERE PC.PlanCriteria=@plancriteriaID";
            var res = Models.Database.SqlQuery<PlanCriteria>(query, new SqlParameter("@plancriteriaID", ID));
            return res.FirstOrDefault();
        }
        public List<PlanCriteria> GetList()
        {
            string query = @"SELECT * FROM PlanCriteria";
            var res = Models.Database.SqlQuery<PlanCriteria>(query);
            return res.ToList();
        }
        public int Add(bool FirstBeenThere, string PlanType, int BudgetInfo, string GroupOfFriends, DateTime DateInterval, string CurrentLocation, string UserLifeStyle) //Veritabanına gitcek kullanıcı tarafından girilecek parametreler
        {
            string query = @"INSERT INTO PlanCriteriasID(FirstBeenThere,PlanType,BudgetInfo,GroupOfFriends,DateInterval,CurrentLocation,UserLifeStyle)
                        Values((SELECT TOP 1 PCID FROM PlanCriteriasID ORDER BY PCID DESC)+1,@firstBeenThere,@planType,@budgetInfo,@groupOfFriends,@dateInterval,@currentLocation,@userLifeStyle)"; //normal sqlserverdaki gibi sql
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@firstBeenThere", FirstBeenThere), new SqlParameter("@planType", PlanType), new SqlParameter("@budgetInfo", BudgetInfo),
                        new SqlParameter("@groupOfFriends", GroupOfFriends), new SqlParameter("@dateInterval", DateInterval), new SqlParameter("@currentLocation", CurrentLocation)); // querye bize gelmiş olan parametreleri ekliyoruz.
            return res;
        }

        public int Update(int PCID, bool FirstBeenThere, string PlanType, int BudgetInfo, string GroupOfFriends, DateTime DateInterval, string CurrentLocation, string UserLifeStyle)
        {
            string query = @"UPDATE PlanCriteriasID SET FirstBeenThere= @firstBeenThere,PlanType= @planType,BudgetInfo=@budgetInfo,GroupOfFriends=@groupOfFriends,
                            DateInterval=@dateInterval,CurrentLocation=@currentLocation WHERE PCID=@pcID";
            return Models.Database.ExecuteSqlCommand(query, new SqlParameter("@pcID", PCID), new SqlParameter("@firstBeenThere", FirstBeenThere), new SqlParameter("@planType", PlanType),
                    new SqlParameter("@budgetInfo", BudgetInfo), new SqlParameter("@groupOfFriends", GroupOfFriends), new SqlParameter("@dateInterval", DateInterval), new SqlParameter("@currentLocation", CurrentLocation), new SqlParameter("@userLifeStyle", UserLifeStyle));
        }


        public int Delete(int PCID)
        {
            var query = @"DELETE FROM PlanCriterias WHERE PCID @planCriteriasID";
            var res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@planCriteriasID", PCID));//bulamadığında 0 dönüyor. bulduğunda 1 dönüyor.
            return (res);
        }
    }
}
