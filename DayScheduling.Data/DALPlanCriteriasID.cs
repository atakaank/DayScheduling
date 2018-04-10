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
    }
}
