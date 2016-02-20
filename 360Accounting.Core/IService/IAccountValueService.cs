using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IAccountValueService : IService<AccountValue>
    {
        AccountValue GetAccountValueBySegment(string segment, long chartId);

        List<AccountValue> GetAccountValuesBySegment(string segment, long chartId);

        List<AccountValue> GetAll(long companyId, string segment);
    }
}
