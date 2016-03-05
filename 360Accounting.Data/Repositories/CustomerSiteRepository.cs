using _360Accounting.Common;
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

        public IEnumerable<CustomerSiteView> GetAllbyCustomerId(long customerId)
        {
            var query = from a in this.Context.CustomerSites
                        join b in this.Context.Taxes on a.TaxCodeId equals b.Id
                        join c in this.Context.CodeCombinitions on a.CodeCombinationId equals c.Id
                        where a.CustomerId == customerId
                        select new CustomerSiteView
                        {
                            CodeCombinationId = a.CodeCombinationId,
                            CodeCombinationName = c.SOBId.ToString(), //TODO: Get the actual thing..
                            CreateBy = a.CreateBy,
                            CreateDate = a.CreateDate,
                            CustomerId = a.CustomerId,
                            EndDate = a.EndDate,
                            Id = a.Id,
                            SiteAddress = a.SiteAddress,
                            SiteContact = a.SiteContact,
                            SiteName = a.SiteName,
                            StartDate = a.StartDate,
                            TaxCodeId = a.TaxCodeId,
                            TaxCodeName = b.TaxName,
                            UpdateBy = a.UpdateBy,
                            UpdateDate = a.UpdateDate
                        };
            return query;
        }

        public IEnumerable<CustomerSite> GetAll(long companyId)
        {
            //TODO: do we need to get all by company id here?
            IEnumerable<CustomerSite> customerSites = this.Context.CustomerSites;
            return customerSites;
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
