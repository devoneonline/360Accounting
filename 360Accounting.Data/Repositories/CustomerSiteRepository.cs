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
    public class CustomerSiteRepository : Repository, ICustomerSiteRepository
    {
        public CustomerSite GetSingle(string id, long companyId)
        {
            IEnumerable<CustomerSite> customerSites = this.Context.CustomerSites;
            CustomerSite custSite = customerSites.FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return custSite;
        }

        public IEnumerable<CustomerSite> GetAllbyCustomerId(long customerId)
        {
            IEnumerable<CustomerSite> CustomerSiteList = this.Context.CustomerSites
                .Where(x => x.CustomerId == customerId);
            return CustomerSiteList;
        }

        public IEnumerable<CustomerSite> GetAll(long companyId)
        {
            //TODO: do we need to get all by company id here?
            IEnumerable<CustomerSite> CustomerSiteList = this.Context.CustomerSites
                .Where(x => x.CustomerId == companyId);
            return CustomerSiteList;
        }

        public string Insert(CustomerSite entity)
        {
            this.Context.CustomerSites.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(CustomerSite entity)
        {
            CustomerSite originalEntity = this.Context.CustomerSites.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.CustomerSites.Remove(this.GetSingle(id, companyId));
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
