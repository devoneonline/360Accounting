using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface ICurrencyService : IService<Currency>
    {
        IEnumerable<Currency> GetAll(long companyId, long sobId, string searchText, bool paging, int? page, string sort, string sortDir);

        IEnumerable<Currency> GetAll(long companyId, long sobId);
    }
}
