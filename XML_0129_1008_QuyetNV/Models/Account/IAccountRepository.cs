using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public interface IAccountRepository
    {
        Account GetAccountByUsername(String username);
        String GetPasswordByUsername(String username);
        IEnumerable<Account> GetAllAccounts();
    }


    public class AccountRepository : IAccountRepository
    {
        private List<Account> _allAccounts;
        private XDocument _accountData;

        public AccountRepository()
        {
            _allAccounts = new List<Account>();

            _accountData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/accounts.xml"));
            var accounts = from account in _accountData.Descendants("account")
                          select new Account(
                              account.Element("username").Value, 
                              account.Element("password").Value
                              );

            _allAccounts.AddRange(accounts.ToList<Account>());

        }
        public Account GetAccountByUsername(string username)
        {
            return _allAccounts.Find(account => account.Username.Equals(username));
        }

        public string GetPasswordByUsername(string username)
        {
            return _allAccounts.Find(account => account.Username.Equals(username)).MD5Password;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _allAccounts;
        }
    }

}
