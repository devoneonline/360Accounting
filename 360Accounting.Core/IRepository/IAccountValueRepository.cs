using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IAccountValueRepository : IRepository<AccountValue>
    {
        AccountValue GetAccountValueBySegment(string segment, long chartId);

        List<AccountValue> GetAccountValuesBySegment(string segment, long chartId);

        IEnumerable<AccountValue> GetAll(long companyId, string segment);
    }
}
