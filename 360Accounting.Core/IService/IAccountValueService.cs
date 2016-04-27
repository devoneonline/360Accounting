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
        AccountValue GetAccountValueBySegment(long chartId, string segment);

        List<AccountValue> GetAccountValuesBySegment(long chartId, long sobId, string segment, int segmentNo, bool fetchSaved);

        List<AccountValue> GetAccountValuesBySegment(long chartId, long sobId, string segment);

        List<AccountValue> GetAccountValuesByChartId(long chartId, long sobId);
    }
}
