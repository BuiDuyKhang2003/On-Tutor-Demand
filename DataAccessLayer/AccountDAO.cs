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
        private static AppDbContext db;

        public static List<Account> GetAllAccounts()
        {
            db = new();
            return db.Accounts.ToList();
        }

        public static Account GetAccountById(int accountId)
        {
            db = new();
            return db.Accounts.Find(accountId) ?? new Account();
        }

        public static Account GetAccountByEmail(string email)
        {
            db = new();
            return db.Accounts.FirstOrDefault(a => a.Email == email) ?? new Account();
        }

        public static void AddAccount(Account account)
        {
            db = new();
            db.Accounts.Add(account);
            db.SaveChanges();
        }

        public static void UpdateAccount(Account account)
        {
            db = new();
            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteAccount(int accountId)
        {
            db = new();
            var account = db.Accounts.Find(accountId);
            if (account != null)
            {
                db.Accounts.Remove(account);
                db.SaveChanges();
            }
        }
    }
}
