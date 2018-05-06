using DayScheduling.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DayScheduling.Entities.Account;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        BLLActivity bllAct = new BLLActivity();
        BLLAccount bllAcc = new BLLAccount();
        public ActionResult LoginPage()
        {
            if(!AccountUser.isLogined)
            return View();

            return RedirectToAction("UserPage", "Home");
        }
        [HttpPost]
        public ActionResult isLoginSuccess(string NameOrEmail, string password)
        {
            var resSuccess = new { Success = "True", Message = "", TargetUrl = Url.Action("UserPage", "Home") };
            var resFail = new { Success = "False", Message = "Invalid email or password", TargetUrl = "" };
            if (bllAcc.LoginIsSuccess(NameOrEmail, password))
            {
                if (Session["Account"] != null)
                {
                    Session.Add("Account", "1");
                }
                else
                {
                    Session["Account"] = "1";
                }
                return Json(resSuccess, JsonRequestBehavior.AllowGet);
            }

            return Json(resFail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult isSignUpSuccess(string UserSurname, string UserName, string Gender, DateTime DOB, string Email, string Phone, string Address, string Job, string Password, string AccountType)
        {
            var resSuccess = new { Success = "True", Message = "ACCOUNT IS ADDED.", TargetUrl = Url.Action("UserPage", "Home") };
            var resFail = new { Success = "False", Message = "EMAIL IS ALREADY TAKEN.", TargetUrl = "" };
            if (bllAcc.AddAccountUser(UserSurname, UserName, Gender, DOB, Email, Phone, Address, Job, Password, AccountType) == 2)
            {
                return Json(resSuccess, JsonRequestBehavior.AllowGet);
            }
            return Json(resFail, JsonRequestBehavior.AllowGet);
        }
    }
}