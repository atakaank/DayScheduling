using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Data
{
    class DALPlaceType
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
    }
}
