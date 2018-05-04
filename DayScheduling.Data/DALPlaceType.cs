using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    public class DALPlaceType
    {
        DaySchedulingModelDataModels Models = new DaySchedulingModelDataModels();
        public PlaceType Get(int ID)
        {
            string query = @"SELECT * FROM PlaceType PT, WHERE PT.PlaceTypeID=@placetypeID";
            var res = Models.Database.SqlQuery<PlaceType>(query, new SqlParameter("@placetypeID", ID));
            return res.FirstOrDefault();
        }
        public List<PlaceType> GetList()
        {
            string query = @"SELECT * FROM PlaceType";
            var res = Models.Database.SqlQuery<PlaceType>(query);
            return res.ToList();
        }
        public int Add(string PlaceType) //Veritabanına gitcek kullanıcı tarafından girilecek parametreler
        {
            string query = @"INSERT INTO PlaceType(PlaceType)
                        Values((SELECT TOP 1 PlaceTypeID FROM PlaceType ORDER BY PlaceTypeID DESC)+10,@placeType)"; //normal sqlserverdaki gibi sql
            int res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@placeType", PlaceType));
            // querye bize gelmiş olan parametreleri ekliyoruz.
            return res;
        }

        public int Update(int PlaceTypeID, string PlaceType)
        {
            string query = @"UPDATE PlaceType SET PlaceType= @placeType WHERE PlaceTypeID=@placeTypeID";
            return Models.Database.ExecuteSqlCommand(query, new SqlParameter("@placeTypeID", PlaceTypeID), new SqlParameter("@placeType", PlaceType));
        }


        public int Delete(int PlaceTypeID)
        {
            var query = @"DELETE FROM PlaceType WHERE PlaceTypeID= @placeTypeID";
            var res = Models.Database.ExecuteSqlCommand(query, new SqlParameter("@placeTypeID", PlaceTypeID));//bulamadığında 0 dönüyor. bulduğunda 1 dönüyor.
            return (res);
        }
    }
}
