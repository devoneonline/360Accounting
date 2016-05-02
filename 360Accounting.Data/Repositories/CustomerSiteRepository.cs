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
            long longId = Convert.ToInt64(id);
            CustomerSite custSite = (from a in this.Context.CustomerSites
                                    join b in this.Context.Customers on a.CustomerId equals b.Id
                                    where b.CompanyId == companyId && a.Id == longId
                                    select a).FirstOrDefault();
            return custSite;
        }

        public IEnumerable<CustomerSiteView> GetAllbyCustomerId(long customerId)
        {
            var query = from a in this.Context.CustomerSites
                        join c in this.Context.CodeCombinitions on a.CodeCombinationId equals c.Id
                        join b in this.Context.Taxes on a.TaxCodeId equals b.Id into cs
                        from d in cs.DefaultIfEmpty()
                        where a.CustomerId == customerId
                        select new CustomerSiteView
                        {
                            CodeCombinationId = a.CodeCombinationId,
                            CodeCombination = c,
                            CreateBy = a.CreateBy,
                            CreateDate = a.CreateDate,
                            CustomerId = a.CustomerId,
                            EndDate = a.EndDate,
                            Id = a.Id,
                            SiteAddress = a.SiteAddress,
                            SiteContact = a.SiteContact,
                            SiteName = a.SiteName,
                            StartDate = a.StartDate,
                            TaxId = a.TaxCodeId,
                            TaxName = d.TaxName,
                            UpdateBy = a.UpdateBy,
                            UpdateDate = a.UpdateDate
                        };
            return query;
        }

        public IEnumerable<CustomerSite> GetAll(long companyId)
        {
            IEnumerable<CustomerSite> customerSites = from a in this.Context.CustomerSites
                                                      join b in this.Context.Customers on a.CustomerId equals b.Id
                                                      where b.CompanyId == companyId
                                                      select a;
            return customerSites;
        }

        public IEnumerable<CustomerSite> GetByCodeCombinitionId(long codeCombinitionId)
        {
            return this.Context.CustomerSites.Where(rec => rec.CodeCombinationId == codeCombinitionId);
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
