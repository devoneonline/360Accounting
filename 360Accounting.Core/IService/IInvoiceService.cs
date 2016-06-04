using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IInvoiceService : IService<Invoice>
    {
        IEnumerable<InvoiceView> GetAll(long companyId, long sobId);

        Invoice GetSingle(long companyId, long sobId, long periodId, long currencyId);

        IEnumerable<Invoice> GetInvoices(long companyId, long sobId, long periodId);

        IEnumerable<Invoice> GetByCurrencyId(long companyId, long sobId, long currencyId);

        List<CustomerSales> CustomerSales(long companyId, long sobId, DateTime fromDate, DateTime toDate, long customerId);
        List<InvoiceAuditTrail> InvoiceAuditTrail(long companyId, long sobId, DateTime fromDate, DateTime toDate);
        List<InvoicePrintout> InvoicePrintout(long companyId, long sobId, DateTime fromDate, DateTime toDate, string invoiceNo, long customerId, long customerSiteId);
        List<PeriodwiseActivity> PeriodwiseActivity(long companyId, long sobId, DateTime fromDate, DateTime toDate, long customerId);
    }
}
