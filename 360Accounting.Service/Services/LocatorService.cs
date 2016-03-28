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
    public class LocatorService : ILocatorService
    {
        private ILocatorRepository repository;

        public LocatorService(ILocatorRepository repo)
        {
            this.repository = repo;
        }

        public LocatorWarehouse GetSingle(long id)
        {
            return this.repository.GetSingle(id);
        }

        public IEnumerable<LocatorWarehouse> GetAllLocatorWarehouses(long locatorId)
        {
            return this.repository.GetAllLocatorWarehouses(locatorId);
        }

        public long Insert(LocatorWarehouse entity)
        {
            return this.repository.Insert(entity);
        }

        public long Update(LocatorWarehouse entity)
        {
            return this.repository.Update(entity);
        }

        public void DeleteLocatorWarehouse(long id)
        {
            this.repository.DeleteLocatorWarehouse(id);
        }

        public IEnumerable<Locator> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public Locator GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<Locator> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Locator entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Locator entity)
        {
            return this.repository.Update(entity);
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
