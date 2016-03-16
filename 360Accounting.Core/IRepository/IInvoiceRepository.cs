using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        IEnumerable<Invoice> GetAll(long companyId, long sobId,
            long periodId, long currencyId);

        Invoice GetSingle(long companyId, long sobId, long periodId, long currencyId);
    }
}
