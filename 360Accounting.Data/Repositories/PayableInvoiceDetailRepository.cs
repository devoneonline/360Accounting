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
    public class PayableInvoiceDetailRepository : Repository, IPayableInvoiceDetailRepository
    {
        public IList<PayableInvoiceDetail> GetAll(long companyId, long invoiceId)
        {
            throw new NotImplementedException();
            //IList<InvoiceDetail> list = this.Context.InvoiceDetails
            //    .Where(x => x.InvoiceId == invoiceId).ToList();
            //return list;
        }

        public PayableInvoiceDetail GetSingle(string id, long companyId)
        {
            PayableInvoiceDetail entity = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return entity;
        }

        public IEnumerable<PayableInvoiceDetail> GetAll(long companyId)
        {
            IEnumerable<PayableInvoiceDetail> list = this.Context.PayableInvoiceDetails;
            return list;
        }

        public string Insert(PayableInvoiceDetail entity)
        {
            this.Context.PayableInvoiceDetails.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(PayableInvoiceDetail entity)
        {
            var originalEntity = this.Context.PayableInvoiceDetails.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.PayableInvoiceDetails.Remove(this.GetSingle(id, companyId));
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
