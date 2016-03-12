using _360Accounting.Common;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Data.Repositories
{
    public class BankAccountRepository : Repository, IBankAccountRepository
    {
        public IEnumerable<BankAccount> GetBankAccounts(string bankId, long companyId)
        {
            var query = from a in this.Context.BankAccounts
                        join b in this.Context.Banks on a.BankId equals b.Id
                        join c in this.Context.SetOfBooks on b.SOBId equals c.Id
                        join d in this.Context.Companies on c.CompanyId equals d.Id
                        where a.BankId == Convert.ToInt64(bankId)
                        select a;

            return query;
        }

        public BankAccount GetSingle(string id, long companyId)
        {
            BankAccount bankAccount = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return bankAccount;
        }

        public IEnumerable<BankAccount> GetAll(long companyId)
        {
            var query = from a in this.Context.BankAccounts
                        join b in this.Context.Banks on a.BankId equals b.Id
                        join c in this.Context.SetOfBooks on b.SOBId equals c.Id
                        join d in this.Context.Companies on c.CompanyId equals d.Id
                        select a;

            return query;
        }

        public string Insert(BankAccount entity)
        {
            this.Context.BankAccounts.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(BankAccount entity)
        {
            var originalEntity = this.Context.Accounts.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.BankAccounts.Remove(this.GetSingle(id, companyId));
            this.Commit();
        }

        public int Count(long companyId)
        {
            return this.GetAll(companyId).Count();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }
    }
}
