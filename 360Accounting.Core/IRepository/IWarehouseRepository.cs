using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        IEnumerable<Warehouse> GetAll(long companyId, long sobId);
    }
}
