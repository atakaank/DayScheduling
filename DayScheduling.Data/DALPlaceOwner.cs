using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    class DALPlaceOwner
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();
        public PlaceOwner Get(int ID)
        {
            string query = @"SELECT * FROM PlaceOwner PO, WHERE PO.PlaceOwnerID=@placeownerID";
            var res = Models.Database.SqlQuery<PlaceOwner>(query, new SqlParameter("@placeownerID", ID));
            return res.FirstOrDefault();
        }
        public List<PlaceOwner> GetList()
        {
            string query = @"SELECT * FROM PlaceOwner";
            var res = Models.Database.SqlQuery<PlaceOwner>(query);
            return res.ToList();
        }
        public int Add(string PlaceOwnerName, string PlaceOwnerSurname, string Gender, string Phone) //Veritabanına gitcek kullanıcı tarafından girilecek parametreler
        {
            string query = @"INSERT INTO PlaceOwner(PlaceOwnerName,PlaceOwnerSurname,Gender,Phone)
                        Values((SELECT TOP 1 PlaceOwnerID FROM PlaceOwner ORDER BY PlaceOwnerID DESC)+1,@placeOwnerName,@placeOwnerSurname,@gender,@phone)"; //normal sqlserverdaki gibi sql
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@placeOwnerName", PlaceOwnerName), new SqlParameter("@placeOwnerSurname", PlaceOwnerSurname), new SqlParameter("@gender", Gender),
                        new SqlParameter("@phone", Phone)); // querye bize gelmiş olan parametreleri ekliyoruz.
            return res;
        }

        public int Update(int PlaceOwnerID, string PlaceOwnerName, string PlaceOwnerSurname, string Gender, string Phone)
        {
            string query = @"UPDATE PlaceOwner SET PlaceOwnerName= @placeOwnerName,PlaceOwnerSurname= @placeOwnerSurname,Gender=@gender,Phone=@phone";
            return Models.Database.ExecuteSqlCommand(query, new SqlParameter("@placeOwnerID", PlaceOwnerID), new SqlParameter("@placeOwnerName", PlaceOwnerName), new SqlParameter("@placeOwnerSurname", PlaceOwnerSurname),
                    new SqlParameter("@gender", Gender), new SqlParameter("@phone", Phone));
        }


        public int Delete(int PlaceOwnerID)
        {
            var query = @"DELETE FROM PlaceOwner WHERE PlaceOWnerID = @placeOwnerID";
            var res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@placeOwnerID", PlaceOwnerID));//bulamadığında 0 dönüyor. bulduğunda 1 dönüyor.
            return (res);
        }
    }
}
