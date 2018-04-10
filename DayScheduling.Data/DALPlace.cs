using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    public class DALPlace
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();

        public Place Get(int ID)
        {
            string query = @"SELECT * FROM Place P, WHERE P.PlaceID=@placeID"; //cilem.akcay@hotmail.com
            var res = Models.Database.SqlQuery<Place>(query, new SqlParameter("@placeID", ID));
            return res.FirstOrDefault();
        }

        public List<Place> GetList()
        {
            string query = @"SELECT * FROM Place";
            var res = Models.Database.SqlQuery<Place>(query);
            return res.ToList();
        }

        public List<string> GetPlaceTypeFromPlaceID(int ID)
        {
            var Id = ID;
            var queryResult =
                from P in Models.Places join PT in Models.PlaceTypes on P.PlaceTypeID equals PT.PlaceTypeID where P.PlaceID == Id select new { name = P.PlaceName, type = PT.PlaceType1, rate = P.PlaceRate, price = P.PlacePrice, description = P.PlaceDescription };
            List<string> result = new List<string>();
            result.Add(queryResult.FirstOrDefault().name);
            result.Add(queryResult.FirstOrDefault().type);
            result.Add(queryResult.FirstOrDefault().rate.ToString());
            result.Add(queryResult.FirstOrDefault().price.ToString());
            result.Add(queryResult.FirstOrDefault().description);
            return result;
            //string sql = @"SELECT PT.PlaceType,P.PlaceRate,P.PlacePrice FROM PlaceType PT, Place P WHERE P.PlaceTypeID = PT.PlaceTypeID AND P.PlaceID = @PlaceID";
            /*var rs = Models.Database.SqlQuery<string>("SELECT PlaceType FROM Place").ToList();*/
            //var result = Models.Database.SqlQuery<denemview>(sql, new SqlParameter("@PlaceID",Id)).ToList().FirstOrDefault();
            //Models.Database.ExecuteSqlCommand(result.ToString());
            //return result.ToString();
        }
        public int Add(string PlaceName, int PlaceTypeID, string PlaceAddress, string Phone, int PlaceRate, int PlacePrice, string PlaceDescription) //Veritabanına gitcek kullanıcı tarafından girilecek parametreler
        {
            string query = @"INSERT INTO Place(PlaceName,PlaceTypeID,PlaceAddress,Phone,PlaceRate,PlacePrice,PlaceDescription)
                        Values((SELECT TOP 1 PlaceID FROM Place ORDER BY PlaceID DESC)+1,@placeName,@placeTypeID,@placeAddress,@phone,@placeRate,@placePrice,@placeDescription)"; //normal sqlserverdaki gibi sql
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@placeName", PlaceName), new SqlParameter("@placeTypeID", PlaceTypeID), new SqlParameter("@placeAddress", PlaceAddress),
                        new SqlParameter("@phone", Phone), new SqlParameter("@placeRate", PlaceRate), new SqlParameter("@placePrice", PlacePrice), new SqlParameter("@placeDescription", PlaceDescription)); // querye bize gelmiş olan parametreleri ekliyoruz.
            return res;
        }

        public int Update(int PlaceID, string PlaceName, int PlaceTypeID, string PlaceAddress, string Phone, int PlaceRate, int PlacePrice, string PlaceDescription)
        {
            string query = @"UPDATE Place SET PlaceName = @placeName,PlaceTypeID = @placeTypeID,PlaceAddress=@placeAddress,Phone=@phone
                            PlaceRate= @placerate, PlacePrice= @placeprice, PlaceDescription= @placeDescription WHERE PlaceID=@PlaceID";
            return Models.Database.ExecuteSqlCommand(query, new SqlParameter("@placeID", PlaceID), new SqlParameter("@placeName", PlaceName), new SqlParameter("@placeTypeID", PlaceTypeID),
                    new SqlParameter("@placeAddress", PlaceAddress), new SqlParameter("@phone", Phone), new SqlParameter("@placeRate", PlaceRate), new SqlParameter("@placeDescription", PlaceDescription));
        }


        public int Delete(int PlaceID)
        {
            var query = @"DELETE FROM Place WHERE PlaceID = @placeID";
            var res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@placeID", PlaceID));//bulamadığında 0 dönüyor. bulduğunda 1 dönüyor.
            return (res);
        }
    }
}
