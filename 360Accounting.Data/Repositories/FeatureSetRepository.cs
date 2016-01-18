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
        public FeatureSet GetSingle(string id)
        {
            FeatureSet featureSet = this.GetAll().FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return featureSet;
        }

        public IEnumerable<FeatureSet> GetAll()
        {
            IEnumerable<FeatureSet> featureSets = this.Context.FeatureSets;
            return featureSets;
        }

        public string Insert(FeatureSet entity)
        {
            this.Context.FeatureSets.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(FeatureSet entity)
        {
            this.Context.FeatureSets.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id)
        {
            this.Context.FeatureSets.Remove(this.GetSingle(id));
            this.Commit();
        }

        public int Count()
        {
            return this.GetAll().Count();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }
    }
}
