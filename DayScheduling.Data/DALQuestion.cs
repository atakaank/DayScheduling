using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    class DALQuestion
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();
        public Question Get(int ID)
        {
            string query = @"SELECT * FROM Question Q, WHERE Q.QuestionID=@questionID";
            var res = Models.Database.SqlQuery<Question>(query, new SqlParameter("@questionID", ID));
            return res.FirstOrDefault();
        }
        public List<Question> GetList()
        {
            string query = @"SELECT * FROM Question";
            var res = Models.Database.SqlQuery<Question>(query);
            return res.ToList();
        }
        public int Add(string QS) //Veritabanına gitcek kullanıcı tarafından girilecek parametreler
        {
            string query = @"INSERT INTO Question(QS)
                        Values((SELECT TOP 1 QuestionID FROM Question ORDER BY QuestionID DESC)+1,@qs)"; //normal sqlserverdaki gibi sql
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@qs", QS));
                        // querye bize gelmiş olan parametreleri ekliyoruz.
            return res;
        }

        public int Update(int QuestionID, string QS)
        {
            string query = @"UPDATE Question SET QS = @qs WHERE ActivityID=@activityID";
            return Models.Database.ExecuteSqlCommand(query, new SqlParameter("@questionID", QuestionID), new SqlParameter("@qs", QS));
        }


        public int Delete(int QuestionID)
        {
            var query = @"DELETE FROM Question WHERE QuestionID= @questionID";
            var res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@questionID", QuestionID));//bulamadığında 0 dönüyor. bulduğunda 1 dönüyor.
            return (res);
        }
    }
}
