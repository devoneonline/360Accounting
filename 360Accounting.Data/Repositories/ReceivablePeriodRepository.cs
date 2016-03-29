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
    public class ReceivablePeriodRepository : Repository, IReceivablePeriodRepository
    {
        public IEnumerable<ReceivablePeriod> GetAll(long companyId, long sobId)
        {
            IEnumerable<ReceivablePeriod> list = this.Context.ReceivablePeriods.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.Status == "Open");
            return list;
        }

        public ReceivablePeriod GetSingle(string id, long companyId)
        {
            ReceivablePeriod receivablePeriod = this.Context.ReceivablePeriods
                .FirstOrDefault(x => x.Id == Convert.ToInt64(id) && x.CompanyId == companyId);
            return receivablePeriod;
        }

        public IEnumerable<ReceivablePeriod> GetAll(long companyId)
        {
            IEnumerable<ReceivablePeriod> list = this.Context.ReceivablePeriods.Where(x => x.CompanyId == companyId);
            return list;
        }

        public string Insert(ReceivablePeriod entity)
        {
            this.Context.ReceivablePeriods.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(ReceivablePeriod entity)
        {
            var originalEntity = this.Context.ReceivablePeriods.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.ReceivablePeriods.Remove(this.GetSingle(id, companyId));
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
