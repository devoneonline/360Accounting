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
    public class MiscellaneousTransactionRepository : Repository, IMiscellaneousTransactionRepository
    {
        public IEnumerable<MiscellaneousTransaction> GetAll(long companyId, long sobId)
        {
            IEnumerable<MiscellaneousTransaction> list = this.Context.MiscellaneousTransactions.Where(x => x.CompanyId == companyId && x.SOBId == sobId);
            return list;
        }

        public IEnumerable<MiscellaneousTransaction> GetAll(long companyId, long sobId, string type, long codeCombinationId)
        {
            IEnumerable<MiscellaneousTransaction> list = this.Context.MiscellaneousTransactions.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.TransactionType == type && x.CodeCombinationId == codeCombinationId);
            return list;
        }

        public MiscellaneousTransaction GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            MiscellaneousTransaction miscTransaction = this.Context.MiscellaneousTransactions.FirstOrDefault(x => x.CompanyId == companyId && x.Id == longId);
            return miscTransaction;
        }

        public IEnumerable<MiscellaneousTransaction> GetAll(long companyId)
        {
            IEnumerable<MiscellaneousTransaction> list = this.Context.MiscellaneousTransactions.Where(x => x.CompanyId == companyId);
            return list;
        }

        public string Insert(MiscellaneousTransaction entity)
        {
            this.Context.MiscellaneousTransactions.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(MiscellaneousTransaction entity)
        {
            MiscellaneousTransaction originalEntity = this.Context.MiscellaneousTransactions.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.MiscellaneousTransactions.Remove(this.GetSingle(id, companyId));
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
