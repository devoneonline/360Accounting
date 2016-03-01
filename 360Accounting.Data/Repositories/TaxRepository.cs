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
    public class TaxRepository : Repository, ITaxRepository
    {
        public Tax GetSingle(string id, long companyId)
        {
            Tax CustomerSite = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return CustomerSite;
        }

        public IEnumerable<Tax> GetAll(long companyId)
        {
            //TODO: do we need to get all by company id here?
            IEnumerable<Tax> CustomerSiteList = this.Context.Taxes;
            return CustomerSiteList;
        }

        public string Insert(Tax entity)
        {
            this.Context.Taxes.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Tax entity)
        {
            Tax originalEntity = this.Context.Taxes.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Taxes.Remove(this.GetSingle(id, companyId));
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
