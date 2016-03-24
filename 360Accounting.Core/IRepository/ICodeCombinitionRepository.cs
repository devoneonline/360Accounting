using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface ICodeCombinitionRepository : IRepository<CodeCombinition>
    {
        IEnumerable<CodeCombinitionView> GetAll(long companyId, long sobId, string searchText, bool paging, int page, string sort, string sortDir);

        List<CodeCombinition> GetAll(long companyId, long sobId);

        IEnumerable<CodeCombinitionView> GetAllCodeCombinitionView(long companyId);

        CodeCombinitionView GetSingle(long id, long companyId);
    }
}
