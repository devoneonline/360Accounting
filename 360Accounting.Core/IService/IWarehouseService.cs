using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IWarehouseService : IService<Warehouse>
    {
        IEnumerable<Warehouse> GetAll(long companyId, long sobId);

        IEnumerable<Warehouse> GetAllforCombo(long companyId, long sobId);
    }
}
