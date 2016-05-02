using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IInvoiceSourceService : IService<InvoiceSource>
    {
        List<InvoiceSource> GetAll(long companyId, long sobId);

        IEnumerable<InvoiceSource> GetByCodeCombinitionId(long companyId, long sobId, long codeCombinitionId);
    }
}
