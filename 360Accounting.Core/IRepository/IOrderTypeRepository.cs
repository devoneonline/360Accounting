using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IOrderTypeRepository : IRepository<OrderType>
    {
        IEnumerable<OrderType> GetOrderTypes(long companyId, long sobId);

        IEnumerable<OrderType> GetOrderTypes(long companyId, long sobId, DateTime date);
    }
}
