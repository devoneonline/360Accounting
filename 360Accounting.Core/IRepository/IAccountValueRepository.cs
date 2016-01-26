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
        List<AccountValue> GetBySegment(string segment, long chartId);

        List<AccountValue> GetBySegment(string segment);
    }
}
