using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class AccountService : IAccountService
    {
        private IAccountRepository repository;

        public AccountService(IAccountRepository repo)
        {
            this.repository = repo;
        }

        public Account GetSingle(string id)
        {
            return this.repository.GetSingle(id);
        }

        public IEnumerable<Account> GetAll()
        {
            return this.GetAll();
        }

        public string Insert(Account entity)
        {
            return this.Insert(entity);
        }

        public string Update(Account entity)
        {
            return this.Update(entity);
        }

        public void Delete(string id)
        {
            this.Delete(id);
        }

        public int Count()
        {
            return this.Count();
        }
    }
}
