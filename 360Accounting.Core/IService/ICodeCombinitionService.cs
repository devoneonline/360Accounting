using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface ICodeCombinitionService : IService<CodeCombinition>
    {
        IEnumerable<CodeCombinitionView> GetAll(string searchText, bool paging, int? page, string sort, string sortDir);
    }
}
