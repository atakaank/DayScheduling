using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayScheduling.Data;

namespace DayScheduling.Entities.Activity
{
    public class vmActivitiyEntities
    {
        public List<vmPartialActivity> Activities {get;set;}

    }
    public class vmPartialActivity
    {
        public TimeSpan FinishTime { get; set; }
        public TimeSpan StartTime { get; set; }
        public Place  place { get; set; }
        public string ActivityPlaceType {get;set;}
        public string DirectionDuration { get; set; }
        public string SourceLat { get; set; }
        public string SourceLong { get; set; }
        public string DestLat { get; set; }
        public string DestLong { get; set; }
    }
}
