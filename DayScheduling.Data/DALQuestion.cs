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
    }
}
