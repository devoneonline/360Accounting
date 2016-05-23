using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IRFQRepository : IRepository<RFQ>
    {
        IEnumerable<RFQDetail> GetAllRFQDetail(long rfqId);
        string InsertRFQDetail(RFQDetail entity);
        string UpdateRFQDetail(RFQDetail entity);
        void DeleteRFQDetail(long id);

        List<RFQView> GetAllRFQs(long companyId, long sobId);

        IEnumerable<RFQ> GetAll(long companyId, long sobId);
    }
}
