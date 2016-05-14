using _360Accounting.Common;
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
            IEnumerable<FeatureSet> featureSets = this.Context.FeatureSets.Where(x => x.CompanyId == companyId);
            return featureSets.ToList();
        }

        public IEnumerable<FeatureSet> GetAll(long companyId, string userRole)
        {
            IEnumerable<FeatureSet> featureSet = this.Context.FeatureSets.Where(x => x.CompanyId == companyId );
            if (userRole != UserRoles.SuperAdmin.ToString())
                return featureSet.Where(x => x.AccessType.ToUpper() != "COMPANY").ToList();
            return featureSet.ToList();
        }

        public IEnumerable<FeatureSet> GetAll()
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
            entity.CreateBy = originalEntity.CreateBy;
            entity.CreateDate = originalEntity.CreateDate;
            entity.AccessType = originalEntity.AccessType;
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
