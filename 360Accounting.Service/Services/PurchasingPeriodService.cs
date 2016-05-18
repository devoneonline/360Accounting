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
    public class PurchasingPeriodService : IPurchasingPeriodService
    {
        private IPurchasingPeriodRepository repository;

        public PurchasingPeriodService(IPurchasingPeriodRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<PurchasingPeriod> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public IEnumerable<PurchasingPeriod> GetByCalendarId(long companyId, long sobId, long calendarId)
        {
            return this.repository.GetByCalendarId(companyId, sobId, calendarId);
        }

        public PurchasingPeriod GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<PurchasingPeriod> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(PurchasingPeriod entity)
        {
            if (entity.IsValid())
                return this.repository.Insert(entity);
            else
                return "Entity is not in valid state";
        }

        public string Update(PurchasingPeriod entity)
        {
            if (entity.IsValid())
                return this.repository.Update(entity);
            else
                return "Entity is not in valid state";
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id, companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }
    }
}
