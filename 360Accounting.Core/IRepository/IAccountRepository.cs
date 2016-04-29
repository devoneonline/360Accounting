using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetAccountBySOBId(string sobId, long companyId);
        
        IEnumerable<AccountView> GetAll(long sobId, long companyId, string searchText, bool paging, int page, string sort, string sortDir);

        long GetAccountIdBySegments(string segments, long companyId, long sobId);
    }
}
