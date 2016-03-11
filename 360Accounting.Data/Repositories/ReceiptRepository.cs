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
    public class ReceiptRepository : Repository, IReceiptRepository
    {
        public IEnumerable<Receipt> GetReceipts(string sobId, string periodId, long companyId)
        {
            var query = from a in this.Context.Receipts
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        where a.SOBId == Convert.ToInt64(sobId) && a.PeriodId == Convert.ToInt64(periodId)
                        select a;
            return query;
        }

        public Receipt GetSingle(string id, long companyId)
        {
            Receipt receipt = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return receipt;
        }

        public IEnumerable<Receipt> GetAll(long companyId)
        {
            var query = from a in this.Context.Receipts
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        select a;

            return query;
        }

        public string Insert(Receipt entity)
        {
            this.Context.Receipts.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Receipt entity)
        {
            var originalEntity = this.Context.Accounts.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Receipts.Remove(this.GetSingle(id, companyId));
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
