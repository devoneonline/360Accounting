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
    public class LocatorRepository : Repository, ILocatorRepository
    {
        public LocatorWarehouse GetSingle(long id)
        {
            LocatorWarehouse locatorWarehouse = this.Context.LocatorWarehouses.FirstOrDefault(x => x.Id == id);
            return locatorWarehouse;
        }

        public IEnumerable<LocatorWarehouse> GetAllLocatorWarehouses(long locatorId)
        {
            IEnumerable<LocatorWarehouse> entityList = this.Context.LocatorWarehouses.Where(x => x.LocatorId == locatorId);
            return entityList;
        }

        public long Insert(LocatorWarehouse entity)
        {
            this.Context.LocatorWarehouses.Add(entity);
            this.Commit();
            return entity.Id;
        }

        public long Update(LocatorWarehouse entity)
        {
            LocatorWarehouse originalEntity = this.Context.LocatorWarehouses.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id;
        }

        public void DeleteLocatorWarehouse(long id)
        {
            this.Context.LocatorWarehouses.Remove(GetSingle(id));
            this.Commit();
        }

        public IEnumerable<Locator> GetAll(long companyId, long sobId)
        {
            IEnumerable<Locator> entityList = GetAll(companyId).Where(x => x.SOBId == sobId);
            return entityList;
        }

        public Locator GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            Locator locator = this.Context.Locators.FirstOrDefault(x => x.CompanyId == companyId && x.Id == longId);
            return locator;
        }

        public IEnumerable<Locator> GetAll(long companyId)
        {
            IEnumerable<Locator> entityList = this.Context.Locators.Where(x => x.CompanyId == companyId);
            return entityList;
        }

        public string Insert(Locator entity)
        {
            this.Context.Locators.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Locator entity)
        {
            Locator originalEntity = this.Context.Locators.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Locators.Remove(this.GetSingle(id, companyId));
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
