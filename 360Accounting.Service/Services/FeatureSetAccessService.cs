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

        public FeatureSetAccess GetSingle(string id)
        {
            return this.repository.GetSingle(id);
        }

        public IEnumerable<FeatureSetAccess> GetAll()
        {
            return this.repository.GetAll();
        }

        public string Insert(FeatureSetAccess entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(FeatureSetAccess entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id)
        {
            this.repository.Delete(id);
        }

        public int Count()
        {
            return this.repository.Count();
        }
    }
}
