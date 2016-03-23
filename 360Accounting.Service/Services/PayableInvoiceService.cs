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
    public class PayableInvoiceService : IPayableInvoiceService
    {
        private IPayableInvoiceRepository repository;

        public PayableInvoiceService(IPayableInvoiceRepository payableInvoiceRepository)
        {
            this.repository = payableInvoiceRepository;
        }

        public PayableInvoice GetSingle(long companyId, long sobId, long periodId)
        {
            return this.repository.GetSingle(companyId, sobId, periodId);
        }

        public IEnumerable<PayableInvoice> GetAll(long companyId, long sobId, long periodId)
        {
            return this.repository.GetAll(companyId, sobId, periodId);
        }

        public PayableInvoice GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<PayableInvoice> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(PayableInvoice entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(PayableInvoice entity)
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
