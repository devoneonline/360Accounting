using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IPayableInvoiceRepository : IRepository<PayableInvoice>
    {
        IEnumerable<PayableInvoice> GetAll(long companyId, long sobId);

        PayableInvoice GetSingle(long companyId, long sobId, long periodId);
    }
}
