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
    public class InventoryPeriodRepository : Repository, IInventoryPeriodRepository
    {
        public IEnumerable<InventoryPeriod> GetAll(long companyId, long sobId)
        {
            IEnumerable<InventoryPeriod> list = this.GetAll(companyId).Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.Status == "Open");
            return list;
        }

        public InventoryPeriod GetSingle(string id, long companyId)
        {
            InventoryPeriod inventoryPeriod = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return inventoryPeriod;
        }

        public IEnumerable<InventoryPeriod> GetAll(long companyId)
        {
            var query = from a in this.Context.InventoryPeriods
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        where c.Id == companyId
                        select a;
            return query;
        }

        public string Insert(InventoryPeriod entity)
        {
            this.Context.InventoryPeriods.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(InventoryPeriod entity)
        {
            var originalEntity = this.Context.InventoryPeriods.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.InventoryPeriods.Remove(this.GetSingle(id, companyId));
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
