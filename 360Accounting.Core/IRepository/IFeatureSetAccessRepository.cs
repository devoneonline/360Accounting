using _360Accounting.Core.Entities;

namespace _360Accounting.Core.Interfaces
{
    public interface IFeatureSetAccessRepository : IRepository<FeatureSetAccess>
    {
        FeatureSetAccess GetSingle(long companyId, string userId);
    }
}
