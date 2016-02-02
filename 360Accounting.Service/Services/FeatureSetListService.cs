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
    public class FeatureSetListService : IFeatureSetListService
    {
        private IFeatureSetListRepository repository;

        public FeatureSetListService(IFeatureSetListRepository featureSetListRepository)
        {
            this.repository = featureSetListRepository;
        }

        public FeatureSetList GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<FeatureSetList> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(FeatureSetList entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(FeatureSetList entity)
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

        public IEnumerable<FeatureSetList> GetByFeatureSetId(long featureSetId)
        {
            return this.repository.GetByFeatureSetId(featureSetId);
        }
    }
}
