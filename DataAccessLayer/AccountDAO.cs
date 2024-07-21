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

        public async static Task<List<Account>> GetAllAccountsAsync()
        {
            db = new();
            return await db.Accounts.ToListAsync();
        }

        public async static Task<Account> GetAccountByIdAsync(int accountId)
        {
            db = new();
            return await db.Accounts.FindAsync(accountId) ?? new Account();
        }

        public async static Task<Account> GetAccountByEmailAsync(string email)
        {
            db = new();
            return await db.Accounts.FirstOrDefaultAsync(a => a.Email.Equals(email)) ?? new Account();
        }

        public async static void AddAccountAsync(Account account)
        {
            db = new();
            await db.Accounts.AddAsync(account);
            await db.SaveChangesAsync();
        }

        public async static void UpdateAccountAsync(Account account)
        {
            db = new();
            db.Entry(account).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async static void DeleteAccountAsync(int accountId)
        {
            db = new();
            var account = await db.Accounts.FindAsync(accountId);
            if (account != null)
            {
                db.Accounts.Remove(account);
                await db.SaveChangesAsync();
            }
        }
    }
}
