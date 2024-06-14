using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        private static AppDbContext db = new();

        public static List<Account> GetAllAccounts()
        {
            return db.Accounts.ToList();
        }

        public static Account GetAccountById(int accountId)
        {
            return db.Accounts.Find(accountId) ?? new Account();
        }

        public static Account GetAccountByEmail(string email)
        {
            return db.Accounts.FirstOrDefault(a => a.Email == email) ?? new Account();
        }

        public static void AddAccount(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
        }

        public static void UpdateAccount(Account account)
        {
            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteAccount(int accountId)
        {
            var account = db.Accounts.Find(accountId);
            if (account != null)
            {
                db.Accounts.Remove(account);
                db.SaveChanges();
            }
        }
    }
}
