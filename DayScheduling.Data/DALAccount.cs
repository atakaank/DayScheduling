using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    public class DALAccount
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();
        public Account Get(int ID)
        {
            string query = @"SELECT * FROM Account A, WHERE A.AccountID=@accountID"; 
            var res = Models.Database.SqlQuery<Account>(query, new SqlParameter("@accountID", ID));
            return res.FirstOrDefault();
        }
        public List<Account> GetList()
        {
            string query = @"SELECT * FROM Account";
            var res = Models.Database.SqlQuery<Account>(query);
            return res.ToList();
        }
        public Account LoginIsSuccess(string UsernameOrEmail,string password)
        {
            string query = @"SELECT * FROM Account A,Users U WHERE A.AccountPassword = @Password AND A.UserID = U.UserID AND U.Email =@Email"; //cilem.akcay@hotmail.com
            var res = Models.Database.SqlQuery<Account>(query,new SqlParameter("@Password",password),new SqlParameter("@Email",UsernameOrEmail));
            return res.FirstOrDefault();
        }
        public void Add()
        {
            string query = @"INSERT INTO USERS(userID,)";
            //Models.Database.ExecuteSqlCommand();
        }
    }
}