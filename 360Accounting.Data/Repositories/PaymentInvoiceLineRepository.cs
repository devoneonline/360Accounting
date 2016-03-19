using _360Accounting.Core.Interfaces;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace _360Accounting.Data.Repositories
{
    public class PaymentInvoiceLineRepository : Repository, IPaymentInvoiceLineRepository
    {
        public PaymentInvoiceLines GetSingle(string id, long companyId)
        {
            PaymentInvoiceLines entity = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return entity;
        }

        public IList<PaymentInvoiceLines> GetAll(long companyId, long headerId)
        {
            IList<PaymentInvoiceLines> entityList = this.Context.PaymentInvoiceLines.Where(x => x.PaymentId == headerId).ToList();
            return entityList;
        }

        public IEnumerable<PaymentInvoiceLines> GetAll(long companyId)
        {
            IEnumerable<PaymentInvoiceLines> entityList = this.Context.PaymentInvoiceLines;
            return entityList;
        }

        public string Insert(PaymentInvoiceLines entity)
        {
            this.Context.PaymentInvoiceLines.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(PaymentInvoiceLines entity)
        {
            var originalEntity = this.Context.PaymentInvoiceLines.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.PaymentInvoiceLines.Remove(this.GetSingle(id, companyId));
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