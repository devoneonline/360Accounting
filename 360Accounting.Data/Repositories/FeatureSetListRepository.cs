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
        public FeatureSetList GetSingle(string id, long companyId)
        {
            FeatureSetList entity = this.GetAll(companyId).FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return entity;
        }

        public IEnumerable<FeatureSetList> GetAll(long companyId)
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

        public void Delete(string id, long companyId)
        {
            this.Context.FeatureSetLists.Remove(this.GetSingle(id, companyId));
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

        public IEnumerable<FeatureSetList> GetByFeatureSetId(long featureSetId)
        {
            IEnumerable<FeatureSetList> list = this.Context.FeatureSetLists.Where(x => x.FeatureSetId == featureSetId);
            return list;
        }

        public void Insert(List<FeatureSetList> entityList)
        {
            foreach(FeatureSetList entity in entityList)
            {
                this.Context.FeatureSetLists.Add(entity);
            }
            this.Commit();
        }
    }
}
