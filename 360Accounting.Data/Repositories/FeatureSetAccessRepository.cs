using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Data.Repositories
{
    public class FeatureSetAccessRepository : Repository, IFeatureSetAccessRepository
    {
        public FeatureSetAccess GetSingle(string id, long companyId)
        {
            FeatureSetAccess entity = this.GetAll(companyId).FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return entity;
        }

        public FeatureSetAccess GetSingle(long companyId, string userId)
        {
            return this.GetAll(companyId).FirstOrDefault(x => x.UserId == Guid.Parse(userId) && x.CompanyId == companyId);
        }

        public IEnumerable<FeatureSetAccess> GetAll(long companyId)
        {
            IEnumerable<FeatureSetAccess> list = this.Context.FeatureSetAccesses;
            return list;
        }

        public string Insert(FeatureSetAccess entity)
        {
            this.Context.FeatureSetAccesses.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(FeatureSetAccess entity)
        {
            this.Context.FeatureSetAccesses.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.FeatureSetAccesses.Remove(this.GetSingle(id, companyId));
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
