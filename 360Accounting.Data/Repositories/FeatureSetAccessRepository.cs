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
        public FeatureSetAccess GetSingle(string id)
        {
            FeatureSetAccess entity = this.GetAll().FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return entity;
        }

        public IEnumerable<FeatureSetAccess> GetAll()
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

        public void Delete(string id)
        {
            this.Context.FeatureSetAccesses.Remove(this.GetSingle(id));
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
