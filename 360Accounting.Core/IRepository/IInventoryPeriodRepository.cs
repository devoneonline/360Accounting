﻿using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IInventoryPeriodRepository : IRepository<InventoryPeriod>
    {
        IEnumerable<InventoryPeriod> GetAll(long companyId, long sobId);

        IEnumerable<InventoryPeriod> GetByCalendarId(long companyId, long sobId, long calendarId);
    }
}
