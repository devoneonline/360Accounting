using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IRFQService : IService<RFQ>
    {
        IEnumerable<RFQDetail> GetAllRFQDetail(long rfqId);
        string InsertRFQDetail(RFQDetail entity);
        string UpdateRFQDetail(RFQDetail entity);
        void DeleteRFQDetail(long id);

        List<RFQView> GetAllRFQs(long companyId, long sobId);
        IEnumerable<RFQ> GetAll(long companyId, long sobId);
    }
}
