using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IWithholdingRepository : IRepository<Withholding>
    {
        IEnumerable<Withholding> GetWithholdings(long companyId, long sobId, long codeCombinitionId, long vendorId);

        IEnumerable<Withholding> GetAll(long companyId, long sobId, long vendorId, long vendorSiteId, DateTime startDate, DateTime endDate);

        IEnumerable<Withholding> GetByCodeCombinitionId(long companyId, long sobId, long codeCombinitionId);
    }
}
