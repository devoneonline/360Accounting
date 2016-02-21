using _360Accounting.Core.Entities;
using System.Collections.Generic;

namespace _360Accounting.Core.Interfaces
{
    public interface IFeatureSetAccessRepository : IRepository<FeatureSetAccess>
    {
        FeatureSetAccess GetByFeatureSetId(long companyId, long featureSetId);

        FeatureSetAccess GetSingle(long companyId, string userId);
    }
}