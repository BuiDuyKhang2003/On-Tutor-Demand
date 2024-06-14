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
        public void AddAccount(Account account) => AccountDAO.AddAccount(account);

        public void DeleteAccount(int accountId) => AccountDAO.DeleteAccount(accountId);

        public Account GetAccountByEmail(string email) => AccountDAO.GetAccountByEmail(email);

        public Account GetAccountById(int accountId) => AccountDAO.GetAccountById(accountId);

        public IEnumerable<Account> GetAllAccounts() => AccountDAO.GetAllAccounts();

        public void UpdateAccount(Account account) => AccountDAO.UpdateAccount(account);
    }
}
