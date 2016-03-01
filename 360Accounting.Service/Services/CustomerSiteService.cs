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
    public class CustomerSiteService : ICustomerSiteService
    {
        private ICustomerSiteRepository repository;

        public CustomerSiteService(ICustomerSiteRepository repo)
        {
            this.repository = repo;
        }

        public CustomerSite GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<CustomerSite> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public IEnumerable<CustomerSite> GetAllbyCustomerId(long customerId)
        {
            return this.repository.GetAllbyCustomerId(customerId);
        }

        public string Insert(CustomerSite entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(CustomerSite entity)
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
