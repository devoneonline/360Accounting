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

        public IEnumerable<PayableInvoice> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
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
            if (entity.IsValid())
                return this.repository.Insert(entity);
            else
                return "Entity is not in valid state";
        }

        public string Update(PayableInvoice entity)
        {
            if (entity.IsValid())
                return this.repository.Update(entity);
            else
                return "Entity is not in valid state";
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id, companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }




        public List<PurchasePrintout> PurchasePrintout(long companyId, long sobId, DateTime fromDate, DateTime toDate, string invoiceNo, long vendorId, long vendorSiteId)
        {
            return this.repository.PurchasePrintout(companyId, sobId, fromDate, toDate, invoiceNo, vendorId, vendorSiteId);
        }
    }
}
