using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface ICalendarService : IService<Calendar>
    {
        IEnumerable<Calendar> GetAll(long companyId, long sobId, string searchText, bool paging, int? page, string sort, string sortDir);

        IEnumerable<Calendar> GetAll(long companyId, long sobId);

        Calendar getCalendarByPeriod(long companyId, long sobId, DateTime? startDate, DateTime? endDate);
    }
}
