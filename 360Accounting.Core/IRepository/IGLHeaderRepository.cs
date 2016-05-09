using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IGLHeaderRepository : IRepository<GLHeader>
    {
        IEnumerable<GLHeaderView> GetAll(long companyId, long sobId);

        GLHeader GetSingle(long CompanyId, long PeriodId, long SOBId, long CurrencyId);

        List<UserwiseEntriesTrail> UserwiseEntriesTrail(long companyId, long sobId, DateTime fromDate, DateTime toDate, Guid? UserId);

        List<AuditTrail> AuditTrail(long companyId, long sobId, DateTime fromDate, DateTime toDate);

        List<Ledger> Ledger(long companyId, long sobId, long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate);

        List<TrialBalance> TrialBalance(long companyId, long sobId, long fromCodeCombinationId, long toCodeCombinationId, long periodId);

        IEnumerable<GLHeader> GetByCurrencyId(long companyId, long sobId, long currencyId);
    }
}
