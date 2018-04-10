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
        public int Add(string UserSurname,string UserName,string Gender,DateTime DOB,string email,string Phone,string Address,string Job,string Password,string AccountType)
        {
            int res = 0;
            string queryUser = @"INSERT INTO Users(UserID,UserSurname,UserName,Gender,DateOfBirth,Email,Phone,UserAddress,Job)
                            VALUES((SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC)+1,@userSurname,@userName,'A',@gender,@dateOfBirth,@email,@phone,@userAddress,@job)";
            string queryAccount = @"INSERT INTO Account(AccountID,UserID,CreatedDate,AccountType,AccountPassword)
                            VALUES ((SELECT TOP 1 AccountID FROM Account ORDER BY AccountID DESC),(SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC)+1),@createdDate,@accountType,@accountPassword";
            res = Models.Database.ExecuteSqlCommand(queryUser, new SqlParameter("@userSurname",UserSurname), new SqlParameter("@userName", UserName), new SqlParameter("@gender", Gender),
                new SqlParameter("@dateOfBirth", DOB), new SqlParameter("@email", email), new SqlParameter("@phone", Phone), new SqlParameter("@userAddress", Address), new SqlParameter("@job", Job));
            return res += Models.Database.ExecuteSqlCommand(queryAccount, new SqlParameter("@createdDate",DateTime.Now), new SqlParameter("@accountType",AccountType), new SqlParameter("@accountPassword",Password));
        }
        //public int add(string surveyname)
        //{
        //    string q = @"insert into Survey(SurveyID,SurveyName) Values((SELECT TOP 1 SurveyID FROM Survey ORDER BY SurveyID DESC)+1,@surveyName)";
        //    Models.Database.ExecuteSqlCommand(q, new SqlParameter("@surveyName", surveyname));
        //}
    }
}