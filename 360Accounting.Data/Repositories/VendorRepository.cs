using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace _360Accounting.Data.Repositories
{
    public class VendorRepository : Repository, IVendorRepository
    {
        public IEnumerable<Vendor> GetAll(long companyId, long sobId, DateTime startDate, DateTime endDate)
        {
            return this.Context.Vendors.Where(x => x.CompanyId == companyId && x.SOBId == sobId && (x.StartDate <= startDate || x.StartDate == null) && (x.EndDate >= endDate || x.EndDate == null));
        }
        
        public Vendor GetSingle(string id, long companyId)
        {
            long longId=Convert.ToInt64(id);
            return this.Context.Vendors.FirstOrDefault(x=> x.CompanyId == companyId && x.Id == longId);
        }

        public IEnumerable<Vendor> GetAll(long companyId)
        {
            return this.Context.Vendors.Where(x => x.CompanyId == companyId);
        }

        public IEnumerable<Vendor> GetAll(long companyId, long sobId)
        {
            return this.Context.Vendors.Where(x => x.CompanyId == companyId && x.SOBId == sobId);
        }

        public string Insert(Vendor entity)
        {
            this.Context.Vendors.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Vendor entity)
        {
            var originalEntity = this.Context.Vendors.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Vendors.Remove(this.GetSingle(id, companyId));
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

        public IEnumerable<VendorSiteView> GetAllSites(long vendorId, long companyId)
        {
            var query = from a in this.Context.VendorSites
                        join b in this.Context.Vendors on a.VendorId equals b.Id
                        join c in this.Context.CodeCombinitions on a.CodeCombinationId equals c.Id
                        where a.VendorId == vendorId && b.CompanyId == companyId
                        select new VendorSiteView
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Address = a.Address,
                            CodeCombination = c,
                            CodeCombinationId = a.CodeCombinationId,
                            Contact = a.Contact,
                            EndDate = a.EndDate,
                            StartDate = a.StartDate,
                            VendorId = a.VendorId
                        };
            return query.ToList();
        }

        public long Insert(VendorSite entity)
        {
            this.Context.VendorSites.Add(entity);
            this.Commit();
            return entity.Id;
        }

        public long Update(VendorSite entity)
        {
            var originalEntity = this.Context.VendorSites.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id;
        }

        public void DeleteSite(long id, long companyId)
        {
            this.Context.VendorSites.Remove(this.Context.VendorSites.Find(id));
            this.Commit();
        }

        public VendorSite GetSingle(long id)
        {
            return this.Context.VendorSites.FirstOrDefault(x => x.Id == id);
        }
    }
}
