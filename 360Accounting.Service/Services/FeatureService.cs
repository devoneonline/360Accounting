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

        public Feature GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<Feature> GetAll( long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Feature entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Feature entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count( long companyId)
        {
            return this.repository.Count(companyId);
        }

        public IEnumerable<Feature> GetMenuItemsByUserId(Guid userId)
        {
            return this.repository.GetMenuItemsByUserId(userId);
        }

        public IEnumerable<Feature> GetSuperAdminMenu()
        {
            return this.repository.GetSuperAdminMenu();
        }
    }
}
