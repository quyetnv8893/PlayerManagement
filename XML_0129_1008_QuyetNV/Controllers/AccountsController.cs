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
using System.Security.Cryptography;
using System.Text;

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
                if (dbAccount.MD5Password.Equals(CalculateMD5Hash(password)))
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        // GET: Authentications/Details/5
      
    }
}
