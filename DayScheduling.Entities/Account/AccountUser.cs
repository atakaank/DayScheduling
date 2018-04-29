using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayScheduling.Data;

namespace DayScheduling.Entities.Account
{
    public static class AccountUser
    {
        public static Data.Account Account { get; set; }
        public static Data.User User { get; set; }
        public static bool isLogined { get; set; }
    }
}
