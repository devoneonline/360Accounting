using _360Accounting.Common;
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

        public IEnumerable<Feature> GetAll(long companyId, string userRole, string accessType)
        {
            if (userRole == UserRoles.SuperAdmin.ToString() && accessType.ToUpper() == "COMPANY")
                return GetAll(companyId);

            var query = (from a in this.Context.FeatureSets
                        join b in this.Context.FeatureSetLists on a.Id equals b.FeatureSetId
                        join c in this.Context.Features on b.FeatureId equals c.Id
                        where a.CompanyId == companyId && a.AccessType.ToUpper() == "COMPANY"
                        select c).ToList();

            List<Feature> featureList = PrepareFeatureList(query);
            return featureList;

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
            var originalEntity = this.Context.Features.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
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

            var query = (from a in this.Context.FeatureSetAccesses
                        join c in this.Context.FeatureSetLists on a.FeatureSetId equals c.FeatureSetId
                        join d in this.Context.Features on c.FeatureId equals d.Id
                        where a.UserId == userId
                        select d).ToList();

            List<Feature> featureList = PrepareFeatureList(query);            
            return featureList;
        }

        public IEnumerable<Feature> GetSuperAdminMenu()
        {
            IEnumerable<Feature> features = this.Context.Features.Where(x => x.Id == 8).Include(x => x.Features);
            return features;
        }

        private List<Feature> PrepareFeatureList(List<Feature> query)
        {
            List<Feature> featureList = new List<Feature>();
            featureList = query.Where(x => x.ParentId == null).ToList();
            foreach (var q in query)
            {
                var f = featureList.FirstOrDefault(x => x.Id == q.ParentId);
                if (f != null)
                {
                    if (f.Features == null)
                        f.Features = new List<Feature>();
                    if (!f.Features.Any(x => x.Id == q.Id))
                        f.Features.Add(q);
                }
            }
            return featureList;
        }

    }
}
