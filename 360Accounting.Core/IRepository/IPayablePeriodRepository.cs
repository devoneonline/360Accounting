﻿using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IPayablePeriodRepository : IRepository<PayablePeriod>
    {
        IEnumerable<PayablePeriod> GetAll(long companyId, long sobId);

        IEnumerable<PayablePeriod> GetByCalendarId(long companyId, long sobId, long calendarId);
    }
}
