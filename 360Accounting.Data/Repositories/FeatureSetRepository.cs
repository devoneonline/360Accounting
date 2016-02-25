using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace _360Accounting.Data.Repositories
{
    public class FeatureSetRepository : Repository, IFeatureSetRepository
    {
        public FeatureSet GetSingle(string id, long companyId)
        {
            FeatureSet featureSet = this.GetAll(companyId).FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return featureSet;
        }

        public IEnumerable<FeatureSet> GetAll(long companyId)
        {
            IEnumerable<FeatureSet> featureSets = this.Context.FeatureSets;
            return featureSets.ToList();
        }

        public string Insert(FeatureSet entity)
        {
            this.Context.FeatureSets.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(FeatureSet entity)
        {
            var originalEntity = this.Context.FeatureSets.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.FeatureSets.Remove(this.GetSingle(id, companyId));
            this.Commit();
        }

        public int Count(long companyId)
        {
            return this.GetAll(companyId).Count();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }
    }
}
