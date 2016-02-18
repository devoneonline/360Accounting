using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IJournalVoucherRepository : IRepository<JournalVoucher>
    {
        IEnumerable<JournalVoucher> GetAll(long companyId, string searchText, bool paging, int page, string sort, string sortDir);

        IEnumerable<JournalVoucherDetail> GetAll(string headerId);

        string Insert(JournalVoucherDetail entity);

        string Update(JournalVoucherDetail entity);

        List<UserwiseEntriesTrail> UserwiseEntriesTrail(long companyId, long sobId, DateTime fromDate, DateTime toDate, Guid? UserId);

        List<AuditTrail> AuditTrail(long companyId, long sobId, DateTime fromDate, DateTime toDate);

        List<Ledger> Ledger(long companyId, long sobId, long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate);

        List<TrialBalance> TrialBalance(long companyId, long sobId, long fromCodeCombinationId, long toCodeCombinationId, long periodId);
    }
}
