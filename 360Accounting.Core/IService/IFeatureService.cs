using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IFeatureService : IService<Feature>
    {
        IEnumerable<Feature> GetAll(long companyId, string userRole, string accessType);
        IEnumerable<Feature> GetMenuItemsByUserId(Guid userId);
        IEnumerable<Feature> GetSuperAdminMenu();
        void InsertCompanyFeatureSet(FeatureSet fs, FeatureSetAccess fsa);

        void UpdateCompanyFeatureSet(FeatureSet fs, IEnumerable<FeatureSetList> oldFeatureSetList);
    }
}
