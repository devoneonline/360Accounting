﻿using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface ICalendarRepository : IRepository<Calendar>
    {
        IEnumerable<Calendar> GetAll(long companyId, long sobId, string searchText, bool paging, int page, string sort, string sortDir);

        IEnumerable<Calendar> GetAll(long companyId, long sobId);

        Calendar getCalendarByPeriod(long companyId, long sobId, DateTime? startDate, DateTime? endDate);

        Calendar getLastCalendarByYear(long companyId, long sobId, int periodYear);
    }
}
