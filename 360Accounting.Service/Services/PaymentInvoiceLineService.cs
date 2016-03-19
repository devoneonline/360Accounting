using _360Accounting.Core;
using _360Accounting.Core.Interfaces;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class PaymentInvoiceLineService : IPaymentInvoiceLineService
    {
        private IPaymentInvoiceLineRepository repository;

        public PaymentInvoiceLineService(IPaymentInvoiceLineRepository repo)
        {
            this.repository = repo;
        }
        
        public PaymentInvoiceLines GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<PaymentInvoiceLines> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(PaymentInvoiceLines entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(PaymentInvoiceLines entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id, companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }

        public IList<PaymentInvoiceLines> GetAll(long companyId, long headerId)
        {
            return this.repository.GetAll(companyId, headerId);
        }
    }
}