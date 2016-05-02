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
    public class InvoiceSourceRepository : Repository, IInvoiceSourceRepository
    {
        public List<InvoiceSource> GetAll(long companyId, long sobId)
        {
            List<InvoiceSource> invoiceSourceList = this.Context.InvoiceSources.Where(x => x.CompanyId == companyId && x.SOBId == sobId).ToList();
            return invoiceSourceList;
        }

        public InvoiceSource GetSingle(string id, long companyId)
        {
            InvoiceSource invoiceSource = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return invoiceSource;
        }

        public IEnumerable<InvoiceSource> GetAll(long companyId)
        {
            IEnumerable<InvoiceSource> invoiceSourceList = this.Context.InvoiceSources.Where(x => x.CompanyId == companyId);
            return invoiceSourceList;
        }

        public IEnumerable<InvoiceSource> GetByCodeCombinitionId(long companyId, long sobId, long codeCombinitionId)
        {
            return this.Context.InvoiceSources.Where(rec => rec.CompanyId == companyId && rec.SOBId == sobId && rec.CodeCombinationId == codeCombinitionId);
        }

        public string Insert(InvoiceSource entity)
        {
            this.Context.InvoiceSources.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(InvoiceSource entity)
        {
            var originalEntity = this.Context.InvoiceSources.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.InvoiceSources.Remove(this.GetSingle(id, companyId));
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
