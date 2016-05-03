using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IReceivablePeriodRepository : IRepository<ReceivablePeriod>
    {
        IEnumerable<ReceivablePeriod> GetAll(long companyId, long sobId);

        IEnumerable<ReceivablePeriod> GetByCalendarId(long companyId, long sobId, long calendarId);
    }
}
