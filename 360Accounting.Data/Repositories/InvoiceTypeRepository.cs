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
    public class InvoiceTypeRepository : Repository, IInvoiceTypeRepository
    {
        public IEnumerable<InvoiceType> GetAll(long companyId, long sobId, DateTime startDate, DateTime endDate)
        {
            return this.GetAll(companyId).Where(x => x.SOBId == sobId && x.DateFrom <= startDate && x.DateTo >= endDate);
        }
        
        public InvoiceType GetSingle(string id, long companyId)
        {
            InvoiceType invoiceType = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return invoiceType;
        }

        public IEnumerable<InvoiceType> GetAll(long companyId)
        {
            var query = from a in this.Context.InvoiceTypes
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        where c.Id == companyId
                        select a;
            return query;
        }

        public IEnumerable<InvoiceType> GetInvoiceTypes(long sobId, long companyId)
        {
            var query = from a in this.Context.InvoiceTypes
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        where a.SOBId == sobId && c.Id == companyId
                        select a;
            return query;
        }

        public string Insert(InvoiceType entity)
        {
            this.Context.InvoiceTypes.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(InvoiceType entity)
        {
            var originalEntity = this.Context.InvoiceTypes.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.InvoiceTypes.Remove(this.GetSingle(id, companyId));
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
