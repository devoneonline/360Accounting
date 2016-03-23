using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.IService
{
    public interface IVendorService : IService<Vendor>
    {
        VendorSite GetSingle(long id);
        long Insert(VendorSite entity);
        long Update(VendorSite entity);
        void DeleteSite(long id, long companyId);
        IEnumerable<VendorSiteView> GetAllSites(long vendorId, long companyId);

        IEnumerable<Vendor> GetAll(long companyId, DateTime startDate, DateTime endDate);
    }
}
