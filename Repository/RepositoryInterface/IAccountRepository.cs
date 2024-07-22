using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account> GetAccountById(int accountId);
        Task<Account> GetAccountByEmail(string email);
        Task<Account> AddAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(int accountId);
    }
}
