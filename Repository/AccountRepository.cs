using BusinessObject;
using DataAccessLayer;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        public void AddAccount(Account account) => AccountDAO.AddAccountAsync(account);

        public void DeleteAccount(int accountId) => AccountDAO.DeleteAccountAsync(accountId);

        public async Task<Account> GetAccountByEmail(string email) => await AccountDAO.GetAccountByEmailAsync(email);

        public async Task<Account> GetAccountById(int accountId) => await AccountDAO.GetAccountByIdAsync(accountId);

        public async Task<IEnumerable<Account>> GetAllAccounts() => await AccountDAO.GetAllAccountsAsync();

        public void UpdateAccount(Account account) => AccountDAO.UpdateAccountAsync(account);
    }
}
