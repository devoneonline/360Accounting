using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _360Accounting.Data.Repositories
{
    public class FeatureRepository : Repository, IFeatureRepository
    {
        public Feature GetSingle(string id)
        {
            return this.GetAll().FirstOrDefault(x => x.Id == Convert.ToInt32(id));
        }

        public IEnumerable<Feature> GetAll()
        {
            return this.Context.Features;
        }

        public string Insert(Feature entity)
        {
            this.Context.Features.Add(entity);
            return this.Context.SaveChanges().ToString();
        }

        public string Update(Feature entity)
        {
            this.Context.Features.Attach(entity);
            return this.Context.SaveChanges().ToString();
        }

        public void Delete(string id)
        {
            this.Context.Features.Remove(this.Context.Features.FirstOrDefault(x => x.Id == Convert.ToInt32(id)));
        }

        public int Count()
        {
            return this.GetAll().Count();
        }
    }
}
