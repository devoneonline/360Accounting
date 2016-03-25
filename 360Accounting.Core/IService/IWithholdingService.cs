using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IWithholdingService : IService<Withholding>
    {
        IEnumerable<Withholding> GetWithholdings(long companyId, long sobId, long codeCombinitionId, long vendorId);
        IEnumerable<Withholding> GetAll(long companyId, long sobId, long vendorId, long vendorSiteId, DateTime startDate, DateTime endDate);
    }
}
