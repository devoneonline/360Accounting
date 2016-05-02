using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IAccountService : IService<Account>
    {
        Account GetAccountBySOBId(string sobId, long companyId);

        IEnumerable<AccountView> GetAll(long sobId, long companyId, string searchText, bool paging, int? page, string sort, string sortDir);

        long GetAccountIdBySegments(string segments, long companyId, long sobId);

        FeatureSetAccessList UserFeatureSet(long featureSetId, long companyId);
    }
}
