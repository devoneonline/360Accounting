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

        public FeatureSetList GetSingle(string id)
        {
            return this.repository.GetSingle(id);
        }

        public IEnumerable<FeatureSetList> GetAll()
        {
            return this.repository.GetAll();
        }

        public string Insert(FeatureSetList entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(FeatureSetList entity)
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

        public IEnumerable<FeatureSetList> GetByFeatureSetId(long featureSetId)
        {
            return this.repository.GetByFeatureSetId(featureSetId);
        }
    }
}
