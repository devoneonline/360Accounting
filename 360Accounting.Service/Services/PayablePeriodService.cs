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
    public class PayablePeriodService : IPayablePeriodService
    {
        private IPayablePeriodRepository repository;

        public PayablePeriodService(IPayablePeriodRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<PayablePeriod> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }
        
        public PayablePeriod GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<PayablePeriod> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(PayablePeriod entity)
        {
            if (entity.IsValid())
                return this.repository.Insert(entity);
            else
                return "Entity is not in valid state";
        }

        public string Update(PayablePeriod entity)
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