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
    public class InvoiceDetailRepository : Repository, IInvoiceDetailRepository
    {
        public IList<InvoiceDetail> GetAll(long companyId, long invoiceId)
        {
            IList<InvoiceDetail> list = this.Context.InvoiceDetails
                .Where(x => x.InvoiceId == invoiceId).ToList();
            return list;
        }
        
        public InvoiceDetail GetSingle(string id, long companyId)
        {
            InvoiceDetail entity = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return entity;
        }

        public IEnumerable<InvoiceDetail> GetAll(long companyId)
        {
            IEnumerable<InvoiceDetail> list = this.Context.InvoiceDetails;
            return list;
        }

        public string Insert(InvoiceDetail entity)
        {
            this.Context.InvoiceDetails.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(InvoiceDetail entity)
        {
            var originalEntity = this.Context.InvoiceDetails.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.InvoiceDetails.Remove(this.GetSingle(id, companyId));
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
