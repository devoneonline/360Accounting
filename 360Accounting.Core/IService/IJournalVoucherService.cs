using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IJournalVoucherService : IService<JournalVoucher>
    {
        IEnumerable<JournalVoucher> GetAll(long companyId, string searchText, bool paging, int? page, string sort, string sortDir);

        IEnumerable<JournalVoucherDetail> GetAll(string headerId);

        string Insert(JournalVoucherDetail entity);

        string Update(JournalVoucherDetail entity);

        List<UserwiseEntriesTrial> UserwiseEntriesTrial(long companyId, long sobId, DateTime fromDate, DateTime toDate, Guid? UserId);
    }
}
