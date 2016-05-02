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
    public class WithholdingRepository : Repository, IWithholdingRepository
    {
        public IEnumerable<Withholding> GetAll(long companyId, long sobId, long vendorId, long vendorSiteId, DateTime startDate, DateTime endDate)
        {
            return this.GetAll(companyId).Where(x => x.SOBId == sobId && x.VendorId == vendorId && x.VendorSiteId == vendorSiteId &&
                (x.DateFrom <= startDate || x.DateFrom == null) && (x.DateTo >= endDate || x.DateTo == null));
        }

        public Withholding GetSingle(string id, long companyId)
        {
            Withholding withholding = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return withholding;
        }

        public IEnumerable<Withholding> GetAll(long companyId)
        {
            var query = from a in this.Context.Withholdings
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        where c.Id == companyId
                        select a;
            return query;
        }

        public IEnumerable<Withholding> GetWithholdings(long companyId, long sobId, long codeCombinitionId, long vendorId)
        {
            var query = from a in this.Context.Withholdings
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        where c.Id == companyId && a.CodeCombinitionId == codeCombinitionId && a.VendorId == vendorId && a.SOBId == sobId
                        select a;
            return query;
        }

        public IEnumerable<Withholding> GetByCodeCombinitionId(long companyId, long sobId, long codeCombinitionId)
        {
            return this.Context.Withholdings.Where(rec => rec.CompanyId == companyId && rec.SOBId == sobId && rec.CodeCombinitionId == codeCombinitionId);
        }

        public string Insert(Withholding entity)
        {
            this.Context.Withholdings.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Withholding entity)
        {
            var originalEntity = this.Context.Withholdings.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Withholdings.Remove(this.GetSingle(id, companyId));
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
