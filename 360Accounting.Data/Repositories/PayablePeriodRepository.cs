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
        public IEnumerable<PayablePeriod> GetAll(long companyId, long sobId)
        {
            IEnumerable<PayablePeriod> list = this.Context.PayablePeriods.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.Status == "Open");
            return list;
        }

        public IEnumerable<PayablePeriod> GetByCalendarId(long companyId, long sobId, long calendarId)
        {
            IEnumerable<PayablePeriod> list = this.Context.PayablePeriods.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.CalendarId == calendarId);
            return list;
        }
        
        public PayablePeriod GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            PayablePeriod payablePeriod = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == longId);
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
