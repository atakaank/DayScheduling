using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayScheduling.Data;

namespace DayScheduling.BLL
{
    public class BLLAccount
    {
        DALAccount dalacc = new DALAccount();
        public bool LoginIsSuccess(string NameOrEmail,string Password)
        {
            return (dalacc.LoginIsSuccess(NameOrEmail, Password) != null);
        }
        public int AddAccountUser(string UserSurname, string UserName, string Gender, DateTime DOB, string email, string Phone, string Address, string Job, string Password, string AccountType)
        {
            int result = dalacc.Add(UserSurname,UserName,Gender,DOB,email,Phone,Address,Job,Password,AccountType);
            return result;
        }
    }
}
