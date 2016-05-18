using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IPurchasingPeriodRepository : IRepository<PurchasingPeriod>
    {
        IEnumerable<PurchasingPeriod> GetAll(long companyId, long sobId);

        IEnumerable<PurchasingPeriod> GetByCalendarId(long companyId, long sobId, long calendarId);
    }
}
