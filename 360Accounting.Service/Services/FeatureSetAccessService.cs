using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class FeatureSetAccessService : IFeatureSetAccessService
    {
        private IFeatureSetAccessRepository repository;

        public FeatureSetAccessService(IFeatureSetAccessRepository featureSetAccessRepository)
        {
            this.repository = featureSetAccessRepository;
        }

        public FeatureSetAccess GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public FeatureSetAccess GetSingle(long companyId, string userId)
        {
            return this.repository.GetSingle(companyId, userId);
        }

        public IEnumerable<FeatureSetAccess> GetAll( long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(FeatureSetAccess entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(FeatureSetAccess entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }
    }
}
