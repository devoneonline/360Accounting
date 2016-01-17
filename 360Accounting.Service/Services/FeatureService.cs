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
    public class FeatureService : IFeatureService
    {
        private IFeatureRepository repository;

        public FeatureService(IFeatureRepository featureRepository)
        {
            this.repository = featureRepository;
        }

        public Feature GetSingle(string id)
        {
            return this.repository.GetSingle(id);
        }

        public IEnumerable<Feature> GetAll()
        {
            return this.repository.GetAll();
        }

        public string Insert(Feature entity)
        {
            return
                this.repository.Insert(entity);
        }

        public string Update(Feature entity)
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
