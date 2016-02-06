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
    public class FeatureSetService : IFeatureSetService
    {
        private IFeatureSetRepository repository;

        public FeatureSetService(IFeatureSetRepository featureSetRepository)
        {
            this.repository = featureSetRepository;
        }

        public FeatureSet GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<FeatureSet> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(FeatureSet entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(FeatureSet entity)
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
