using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IGLLineRepository : IRepository<GLLines>
    {
        IList<GLLines> GetAll(long companyId, long headerId);

        IEnumerable<GLLines> GetAllByCodeCombinitionId(long codeCombinitionId);
    }
}
