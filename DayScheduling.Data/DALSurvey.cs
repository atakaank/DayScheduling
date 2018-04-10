using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    class DALSurvey
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();
        public Survey Get(int ID)
        {
            string query = @"SELECT * FROM Survey S, WHERE S.SurveyID=@surveyID";
            var res = Models.Database.SqlQuery<Survey>(query, new SqlParameter("@surveyID", ID));
            return res.FirstOrDefault();
        }
        public List<Survey> GetList()
        {
            string query = @"SELECT * FROM Survey";
            var res = Models.Database.SqlQuery<Survey>(query);
            return res.ToList();
        }
    }
}
