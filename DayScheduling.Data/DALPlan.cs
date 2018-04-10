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
    }
}
