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
        public int Add(string SurveyName) //Veritabanına gitcek kullanıcı tarafından girilecek parametreler
        {
            string query = @"INSERT INTO Survey(SurveyName)
                        Values((SELECT TOP 1 SurveyID FROM Survey ORDER BY SurveyID DESC)+1,@surveyName)"; //normal sqlserverdaki gibi sql
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@surveyName", SurveyName));
            // querye bize gelmiş olan parametreleri ekliyoruz.
            return res;
        }

        public int Update(int SurveyID, string SurveyName)
        {
            string query = @"UPDATE Survey SET SurveyName = @surveyName WHERE SurveyID=@surveyID";
            return Models.Database.ExecuteSqlCommand(query, new SqlParameter("@surveyID", SurveyID), new SqlParameter("@surveyID", SurveyName));
        }


        public int Delete(int SurveyID)
        {
            var query = @"DELETE FROM Survey WHERE SurveyID= @surveyID";
            var res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@surveyID", SurveyID));//bulamadığında 0 dönüyor. bulduğunda 1 dönüyor.
            return (res);
        }
    }
}
