using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IMiscellaneousTransactionRepository : IRepository<MiscellaneousTransaction>
    {
        IEnumerable<MiscellaneousTransaction> GetAll(long companyId, long sobId);

        IEnumerable<MiscellaneousTransaction> GetAll(long companyId, long sobId, string type, long codeCombinationId);
    }
}
