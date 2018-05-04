using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    public class DALUser
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();
        public User Get(int ID)
        {
            string query = @"SELECT * FROM Users U WHERE U.UserID=@userID";
            var res = Models.Database.SqlQuery<User>(query, new SqlParameter("@userID", ID));
            return res.FirstOrDefault();
        }
        public List<User> GetList()
        {
            string query = @"SELECT * FROM Users";
            var res = Models.Database.SqlQuery<User>(query);
            return res.ToList();
        }
        public int Add(string UserSurname, string UserName, string UserLifeStyle, string Gender, DateTime DateOfBirth, string Email, string Phone, string UserAddress, string Job) //Veritabanına gitcek kullanıcı tarafından girilecek parametreler
        {
            string query = @"INSERT INTO Users(UserSurname,UserName,UserLifeStyle,Gender,DateOfBirth,Email,Phone,UserAddress,Job)
                        Values((SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC)+1,@userSurname,@userName,@userLifeStyle,@gender,@dateOfBirth,@email,@phone,@userAddress,@job)"; //normal sqlserverdaki gibi sql
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@userSurname", UserSurname), new SqlParameter("@userName", UserName), new SqlParameter("@userLifeStyle", UserLifeStyle),
                        new SqlParameter("@gender", Gender), new SqlParameter("@dateOfBirth", DateOfBirth), new SqlParameter("@email", Email), new SqlParameter("@phone", Phone), new SqlParameter("@userAddress", UserAddress), new SqlParameter("@job", Job)); // querye bize gelmiş olan parametreleri ekliyoruz.
            return res;
        }

        public int Update(int UserID, string UserSurname, string UserName, string UserLifeStyle, string Gender, DateTime DateOfBirth, string Email, string Phone, string UserAddress, string Job)
        {
            string query = @"UPDATE Users SET UserSurname= @userSurname, UserName= @userName,UserLifeStyle=@userLifeStyle,Gender=@gender,
                            DateOfBirth=@dateOfBirth, Email=@email, Phone=@phone, UserAddress=@userAddress, Job=@job WHERE UserID=@userID";
            return Models.Database.ExecuteSqlCommand(query, new SqlParameter("@userID", UserID), new SqlParameter("@userSurname", UserSurname), new SqlParameter("@userName", UserName),
                    new SqlParameter("@userLifeStyle", UserLifeStyle), new SqlParameter("@gender", Gender), new SqlParameter("@dateOfBirth", DateOfBirth), new SqlParameter("@email", Email), new SqlParameter("@phone", Phone), new SqlParameter("@userAddress", UserAddress), new SqlParameter("@job", Job));
        }


        public int Delete(int UserID)
        {
            var query = @"DELETE FROM Users WHERE UserID = @userID";
            var res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@userID", UserID));//bulamadığında 0 dönüyor. bulduğunda 1 dönüyor.
            return (res);
        }
    }
}
