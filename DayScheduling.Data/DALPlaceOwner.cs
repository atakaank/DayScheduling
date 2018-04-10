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
    }
}
