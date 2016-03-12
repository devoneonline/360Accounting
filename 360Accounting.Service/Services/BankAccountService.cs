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
    public class BankAccountService : IBankAccountService
    {
        private IBankAccountRepository repository;

        public BankAccountService(IBankAccountRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<BankAccount> GetBankAccounts(long bankId, long companyId)
        {
            return this.repository.GetBankAccounts(bankId, companyId);
        }

        public BankAccount GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<BankAccount> GetAll( long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(BankAccount entity)
        {
            if (entity.IsValid())
                return this.repository.Insert(entity);
            else
                return "Entity is not in valid state";
        }

        public string Update(BankAccount entity)
        {
            if (entity.IsValid())
                return this.repository.Update(entity);
            else
                return "Entity is not in valid state";
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count( long companyId)
        {
            return this.repository.Count(companyId);
        }
    }
}
