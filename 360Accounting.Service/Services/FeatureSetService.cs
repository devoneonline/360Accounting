using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class FeatureSetService : IService<FeatureSet>
    {
        private IFeatureSetRepository repository;

        public FeatureSetService(IFeatureSetRepository featureSetRepository)
        {
            this.repository = featureSetRepository;
        }

        public FeatureSet GetSingle(string id)
        {
            return this.repository.GetSingle(id);
        }

        public IEnumerable<FeatureSet> GetAll()
        {
            return this.repository.GetAll();
        }

        public string Insert(FeatureSet entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(FeatureSet entity)
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
