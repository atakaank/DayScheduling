using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    class DALPlan
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();

        public Plan Get(int ID)
        {
            string query = @"SELECT * FROM Plan P, WHERE P.PlanID=@planID"; //cilem.akcay@hotmail.com
            var res = Models.Database.SqlQuery<Plan>(query, new SqlParameter("@planID", ID));
            return res.FirstOrDefault();
        }
        public List<Plan> GetList()
        {
            string query = @"SELECT * FROM Plan";
            var res = Models.Database.SqlQuery<Plan>(query);
            return res.ToList();
        }
        public int Add(string PlanName, DateTime PlanDate, string PlanType, int PlanPopularity, bool PlanComplete, int PlanRate) //Veritabanına gitcek kullanıcı tarafından girilecek parametreler
        {
            string query = @"INSERT INTO Plan(PlanName,PlanDate,PlanType,PlanPopularity,PlanComplete,PlanRate)
                        Values((SELECT TOP 1 PlanID FROM Plan ORDER BY PlanID DESC)+1,@planName,@planDate,@planType,@planPopularity,@planComplete,@planRate)"; //normal sqlserverdaki gibi sql
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@planName", PlanName), new SqlParameter("@planDate", PlanDate), new SqlParameter("@planType", PlanType),
                        new SqlParameter("@planPopularity", PlanPopularity), new SqlParameter("@planComplete", PlanComplete), new SqlParameter("@planRate", PlanRate)); // querye bize gelmiş olan parametreleri ekliyoruz.
            return res;
        }

        public int Update(int PlanID,string PlanName, DateTime PlanDate, string PlanType, int PlanPopularity, bool PlanComplete, int PlanRate)
        {
            string query = @"UPDATE Plan SET PlanName = @planName,PlanDate= @planDate,PlanType=@planType,PlanPopularity=@planPopularity,
                            PlanComplete=@planComplete,PlanRate=@planRate WHERE PlanID=@planID";
            return Models.Database.ExecuteSqlCommand(query, new SqlParameter("@planID", PlanID), new SqlParameter("@planName", PlanName), new SqlParameter("@planType", PlanType),
                    new SqlParameter("@planPopularity", PlanPopularity), new SqlParameter("@planComplete", PlanComplete), new SqlParameter("@planRate", PlanRate));
        }


        public int Delete(int PlanID)
        {
            var query = @"DELETE FROM Plan WHERE PlanID = @planID";
            var res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@planID", PlanID));//bulamadığında 0 dönüyor. bulduğunda 1 dönüyor.
            return (res);
        }
    }
}
