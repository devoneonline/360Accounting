using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace _360Accounting.Data.Repositories
{
    public class FeatureRepository : Repository, IFeatureRepository
    {
        public Feature GetSingle(string id, long companyId)
        {
            Feature feature = this.GetAll(companyId).FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return feature;
        }

        public IEnumerable<Feature> GetAll(long companyId)
        {
            IEnumerable<Feature> featureList = (from a in this.Context.Features
                                                select a).Include(x => x.Features);
            return featureList.Where(x => x.ParentId == null);
        }

        public string Insert(Feature entity)
        {
            this.Context.Features.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Feature entity)
        {
            this.Context.Features.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Features.Remove(this.GetSingle(id, companyId));
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

        public IEnumerable<Feature> GetMenuItemsByUserId(Guid userId)
        {
            IEnumerable<Feature> featureList = (from a in this.Context.FeatureSetAccesses
                                                join b in this.Context.FeatureSets on a.FeatureSetId equals b.Id
                                                join c in this.Context.FeatureSetLists on b.Id equals c.FeatureSetId
                                                join d in this.Context.Features on c.FeatureId equals d.Id
                                                where a.UserId == userId
                                                select d).Include(x => x.Features);
            return featureList.Where(x => x.ParentId == null);
        }

        public IEnumerable<Feature> GetSuperAdminMenu()
        {
            return this.Context.Features.Where(x => x.Id == 8).Include(x => x.Features);
        }
    }
}
