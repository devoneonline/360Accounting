using _360Accounting.Core.Entities;
using System.Collections.Generic;

namespace _360Accounting.Core.Interfaces
{
    public interface IFeatureSetListRepository : IRepository<FeatureSetList>
    {
        IEnumerable<FeatureSetList> GetByFeatureSetId(long featureSetId);
        void Insert(List<FeatureSetList> entityList);

        void Delete(FeatureSetList featureSetList);
    }
}
