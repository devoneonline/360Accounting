using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class PayableInvoiceDetailService : IPayableInvoiceDetailService
    {
        private IPayableInvoiceDetailRepository repository;

        public PayableInvoiceDetailService(IPayableInvoiceDetailRepository payableInvoiceDetailRepository)
        {
            this.repository = payableInvoiceDetailRepository;
        }

        public IList<PayableInvoiceDetail> GetAll(long companyId, long invoiceId)
        {
            return this.repository.GetAll(companyId, invoiceId);
        }

        public PayableInvoiceDetail GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<PayableInvoiceDetail> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(PayableInvoiceDetail entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(PayableInvoiceDetail entity)
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
    }
}
