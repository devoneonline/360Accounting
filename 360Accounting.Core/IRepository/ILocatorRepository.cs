using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface ILocatorRepository : IRepository<Locator>
    {
        LocatorWarehouse GetSingle(long id);
        IEnumerable<LocatorWarehouse> GetAllLocatorWarehouses(long locatorId);
        long Insert(LocatorWarehouse entity);
        long Update(LocatorWarehouse entity);
        void DeleteLocatorWarehouse(long id);

        IEnumerable<Locator> GetAll(long companyId, long sobId);
    }
}
