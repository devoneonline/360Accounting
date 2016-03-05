using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IGLHeaderService : IService<GLHeader>
    {
        GLHeader GetSingle(long CompanyId, long PeriodId, long SOBId, long CurrencyId);
        IEnumerable<GLHeader> GetAll(long companyId, long sobId, long periodId, long currencyId);
    }
}
