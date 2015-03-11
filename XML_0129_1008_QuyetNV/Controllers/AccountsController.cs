using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlayerManagement.Models;
using System.Web.Security;
using System.Threading.Tasks;

namespace PlayerManagement.Controllers
{
    public class AccountsController : Controller
    {
        private IAccountRepository db = new AccountRepository();
       
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Username,MD5Password")] Account account)
        {
            bool isPersistent = true;
            if (ModelState.IsValid)
            {
                if (isValid(account.Username, account.MD5Password))
                {                    
                    FormsAuthentication.SetAuthCookie(account.Username, false);
                    return RedirectToAction("Index", "Players");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect");
                }
            }
            return View(account);
        }
   
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Players");
        }
        private bool isValid(string username, string password)
        {
            bool isValid = false;
            var accounts = db.GetAllAccounts();
            var dbAccount = accounts.FirstOrDefault(account => account.Username.Equals(username));
            if (dbAccount != null)
            {
                if (dbAccount.MD5Password.Equals(password))
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        // GET: Authentications/Details/5
      
    }
}
