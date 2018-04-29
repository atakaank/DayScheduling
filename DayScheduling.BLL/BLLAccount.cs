using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayScheduling.Data;
using DayScheduling.Entities.Account;

namespace DayScheduling.BLL
{
    public class BLLAccount
    {
        DALAccount dalacc = new DALAccount();
        DALUser daluser = new DALUser();
        public bool LoginIsSuccess(string NameOrEmail,string Password)
        {
            Account acc = dalacc.LoginIsSuccess(NameOrEmail, Password);
            if(acc != null)
            {
                AccountUser.Account = acc;
                AccountUser.User = daluser.Get(acc.UserID);
                AccountUser.isLogined = true;
                return AccountUser.isLogined;
            }
            return false;
        }
        public int AddAccountUser(string UserSurname, string UserName, string Gender, DateTime DOB, string email, string Phone, string Address, string Job, string Password, string AccountType)
        {
            int result = dalacc.Add(UserSurname,UserName,Gender,DOB,email,Phone,Address,Job,Password,AccountType);
            return result;
        }
    }
}
