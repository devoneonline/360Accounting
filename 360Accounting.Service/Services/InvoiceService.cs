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
    public class InvoiceService : IInvoiceService
    {
        private IInvoiceRepository repository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this.repository = invoiceRepository;
        }

        public Invoice GetSingle(long companyId, long sobId, long periodId, long currencyId)
        {
            return this.repository.GetSingle(companyId, sobId, periodId, currencyId);
        }

        public IEnumerable<InvoiceView> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public IEnumerable<Invoice> GetInvoices(long companyId, long sobId, long periodId)
        {
            return this.repository.GetInvoices(companyId, sobId, periodId);
        }

        public Invoice GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<Invoice> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Invoice entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Invoice entity)
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

        public IEnumerable<Invoice> GetByCurrencyId(long companyId, long sobId, long currencyId)
        {
            return this.GetByCurrencyId(companyId, sobId, currencyId);
        }


        public List<CustomerSales> CustomerSales(long companyId, long sobId, DateTime fromDate, DateTime toDate, long customerId)
        {
            return this.repository.CustomerSales(companyId, sobId, fromDate, toDate, customerId);
        }

        public List<InvoiceAuditTrail> InvoiceAuditTrail(long companyId, long sobId, DateTime fromDate, DateTime toDate)
        {
            return this.repository.InvoiceAuditTrail(companyId, sobId, fromDate, toDate);
        }

        public List<InvoicePrintout> InvoicePrintout(long companyId, long sobId, DateTime fromDate, DateTime toDate, string invoiceNo, long customerId, long customerSiteId)
        {
            return this.repository.InvoicePrintout(companyId, sobId, fromDate, toDate, invoiceNo, customerId, customerSiteId);
        }

        public List<PeriodwiseActivity> PeriodwiseActivity(long companyId, long sobId, DateTime fromDate, DateTime toDate, long customerId)
        {
            return this.repository.PeriodwiseActivity(companyId, sobId, fromDate, toDate, customerId);
        }
    }
}
