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
    public class FeatureSetListRepository : Repository, IFeatureSetListRepository
    {
        public FeatureSetList GetSingle(string id)
        {
            FeatureSetList entity = this.GetAll().FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return entity;
        }

        public IEnumerable<FeatureSetList> GetAll()
        {
            IEnumerable<FeatureSetList> list = this.Context.FeatureSetLists;
            return list;
        }

        public string Insert(FeatureSetList entity)
        {
            this.Context.FeatureSetLists.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(FeatureSetList entity)
        {
            this.Context.FeatureSetLists.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id)
        {
            this.Context.FeatureSetLists.Remove(this.GetSingle(id));
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

        public IEnumerable<FeatureSetList> GetByFeatureSetId(long featureSetId)
        {
            IEnumerable<FeatureSetList> list = this.Context.FeatureSetLists.Where(x => x.FeatureSetId == featureSetId);
            return list;
        }
    }
}
