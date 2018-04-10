using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    class DALUser
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();
        public User Get(int ID)
        {
            string query = @"SELECT * FROM User U, WHERE U.User=@userID";
            var res = Models.Database.SqlQuery<User>(query, new SqlParameter("@userID", ID));
            return res.FirstOrDefault();
        }
        public List<User> GetList()
        {
            string query = @"SELECT * FROM User";
            var res = Models.Database.SqlQuery<User>(query);
            return res.ToList();
        }
    }
}
