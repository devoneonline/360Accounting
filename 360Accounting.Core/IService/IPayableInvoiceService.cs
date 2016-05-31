using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IPayableInvoiceService : IService<PayableInvoice>
    {
        IEnumerable<PayableInvoice> GetAll(long companyId, long sobId);

        PayableInvoice GetSingle(long companyId, long sobId, long periodId);

        List<PurchasePrintout> PurchasePrintout(long companyId, long sobId, DateTime fromDate, DateTime toDate, string invoiceNo, long vendorId, long vendorSiteId);
    }
}
