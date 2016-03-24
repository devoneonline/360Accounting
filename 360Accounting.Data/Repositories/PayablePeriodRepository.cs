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
    public class PayablePeriodRepository : Repository, IPayablePeriodRepository
    {
        public PayablePeriod GetSingle(string id, long companyId)
        {
            PayablePeriod payablePeriod = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return payablePeriod;
        }

        public IEnumerable<PayablePeriod> GetAll(long companyId)
        {
            var query = from a in this.Context.PayablePeriods
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        where c.Id == companyId
                        select a;
            return query;
        }

        public IEnumerable<PayablePeriod> GetAll(long companyId, long sobid)
        {
            var query = from a in this.Context.PayablePeriods
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        where c.Id == companyId && a.SOBId == sobid
                        select a;
            return query;

        }

        public string Insert(PayablePeriod entity)
        {
            this.Context.PayablePeriods.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(PayablePeriod entity)
        {
            var originalEntity = this.Context.PayablePeriods.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.PayablePeriods.Remove(this.GetSingle(id, companyId));
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
