using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IFeatureRepository : IRepository<Feature>
    {
        IEnumerable<Feature> GetMenuItemsByUserId(Guid userId);
        IEnumerable<Feature> GetSuperAdminMenu();
        IEnumerable<Feature> GetAll(long companyId, string userRole, string accessType);
    }
}
