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
    public class PurchasingPeriodRepository : Repository, IPurchasingPeriodRepository
    {
        public IEnumerable<PurchasingPeriod> GetAll(long companyId, long sobId)
        {
            //IEnumerable<PurchasingPeriod> list = this.GetAll(companyId).Where(x => x.SOBId == sobId && x.Status == "Open");
            IEnumerable<PurchasingPeriod> list = this.GetAll(companyId).Where(x => x.SOBId == sobId);
            return list;
        }

        public IEnumerable<PurchasingPeriod> GetByCalendarId(long companyId, long sobId, long calendarId)
        {
            IEnumerable<PurchasingPeriod> list = this.GetAll(companyId).Where(x => x.SOBId == sobId && x.CalendarId == calendarId);
            return list;
        }

        public PurchasingPeriod GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            PurchasingPeriod purchasingPeriod = this.GetAll(companyId).FirstOrDefault(x => x.Id == longId);
            return purchasingPeriod;
        }

        public IEnumerable<PurchasingPeriod> GetAll(long companyId)
        {
            return this.Context.PurchasingPeriods.Where(rec => rec.CompanyId == companyId);
        }

        public string Insert(PurchasingPeriod entity)
        {
            this.Context.PurchasingPeriods.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(PurchasingPeriod entity)
        {
            var originalEntity = this.Context.PurchasingPeriods.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.PurchasingPeriods.Remove(this.GetSingle(id, companyId));
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
