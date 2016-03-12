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
        AccountValue GetAccountValueBySegment(long chartId, string segment);

        List<AccountValue> GetAccountValuesBySegment(long chartId, string segment);

        //IEnumerable<AccountValue> GetAll(long companyId, string segment);
    }
}
