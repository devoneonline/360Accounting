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
    public class InventoryPeriodService : IInventoryPeriodService
    {
        private IInventoryPeriodRepository repository;

        public InventoryPeriodService(IInventoryPeriodRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<InventoryPeriod> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public IEnumerable<InventoryPeriod> GetByCalendarId(long companyId, long sobId, long calendarId)
        {
            return this.repository.GetByCalendarId(companyId, sobId, calendarId);
        }

        public InventoryPeriod GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<InventoryPeriod> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(InventoryPeriod entity)
        {
            if (entity.IsValid())
                return this.repository.Insert(entity);
            else
                return "Entity is not in valid state";
        }

        public string Update(InventoryPeriod entity)
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
