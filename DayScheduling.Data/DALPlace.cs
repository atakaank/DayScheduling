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

        public Place getBreakfastPlace(int provinceID, int budget, string Popularity, int NOF)
        {
            string query = @"SELECT * FROM Place P WHERE P.ProvinceID = @provinceID AND P.PlacePrice <= @budget AND P.PlacePopularityID = @Popularity AND((P.NumberOfPerson>=@NOF) OR (P.NumberOfPerson = 0)) AND (P.PlaceTypeID = 10)";
            var res = Models.Database.SqlQuery<Place>(query, new SqlParameter("@provinceID", provinceID), new SqlParameter("@budget", budget),new SqlParameter("@Popularity", Popularity), new SqlParameter("@NOF", NOF));
            return res.FirstOrDefault();
        }
        public Place getCulturelPlace(int provinceID, int budget, string Popularity, int NOF)
        {
            string query = @"SELECT * FROM Place P WHERE P.ProvinceID = @provinceID AND P.PlacePrice <= @budget AND P.PlacePopularityID = @Popularity AND((P.NumberOfPerson>=@NOF) OR (P.NumberOfPerson = 0)) AND (P.PlaceTypeID = 140)";
            var res = Models.Database.SqlQuery<Place>(query, new SqlParameter("@provinceID", provinceID), new SqlParameter("@budget", budget), new SqlParameter("@Popularity", Popularity), new SqlParameter("@NOF", NOF));
            return res.FirstOrDefault();
        }
        public Place getShoppingPlace(int provinceID, int budget, string Popularity, int NOF)
        {
            string query = @"SELECT * FROM Place P WHERE P.ProvinceID = @provinceID AND P.PlacePrice <= @budget AND P.PlacePopularityID = @Popularity AND((P.NumberOfPerson>=@NOF) OR (P.NumberOfPerson = 0)) AND (P.PlaceTypeID = 70)";
            var res = Models.Database.SqlQuery<Place>(query, new SqlParameter("@provinceID", provinceID), new SqlParameter("@budget", budget), new SqlParameter("@Popularity", Popularity), new SqlParameter("@NOF", NOF));
            return res.FirstOrDefault();
        }

        public Place getHistoricSites(int provinceID, int budget, string Popularity, int NOF)
        {
            string query = @"SELECT * FROM Place P WHERE P.ProvinceID = @provinceID AND P.PlacePrice <= @budget AND P.PlacePopularityID = @Popularity AND((P.NumberOfPerson>=@NOF) OR (P.NumberOfPerson = 0)) AND (P.PlaceTypeID = 80)";
            var res = Models.Database.SqlQuery<Place>(query, new SqlParameter("@provinceID", provinceID), new SqlParameter("@budget", budget), new SqlParameter("@Popularity", Popularity), new SqlParameter("@NOF", NOF));
            return res.FirstOrDefault();
        }

    }
}
