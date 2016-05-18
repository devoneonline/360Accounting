using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        IEnumerable<Buyer> GetAll(long companyId, long sobId, DateTime startDate, DateTime endDate);
        IEnumerable<Buyer> GetAll(long companyId, long sobId);
    }
}
