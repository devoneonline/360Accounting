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
        IEnumerable<CodeCombinitionView> GetAll(string searchText, bool paging, int page, string sort, string sortDir);
    }
}
