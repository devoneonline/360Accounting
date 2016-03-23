using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IReceiptRepository : IRepository<Receipt>
    {
        IEnumerable<ReceiptView> GetReceipts(long sobId, long periodId, long customerId, long currencyId, long companyId);

        IEnumerable<ReceiptView> GetReceipts(long sobId, long bankId, long bankAccountId, DateTime? date = null);
    }
}
