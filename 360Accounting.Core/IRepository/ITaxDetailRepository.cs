using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface ITaxDetailRepository : IRepository<TaxDetail>
    {
        IList<TaxDetail> GetAll(long companyId, long taxId);

        IEnumerable<TaxDetail> GetByCodeCombinitionId(long codeCombinitionId);
    }
}
