using _360Accounting.Core.Interfaces;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace _360Accounting.Data.Repositories
{
    public class GLLineRepository:Repository, IGLLineRepository
    {
        public GLLines GetSingle(string id, long companyId)
        {
            GLLines entity = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return entity;
        }

        public IEnumerable<GLLines> GetAll(long companyId)
        {
            IEnumerable<GLLines> entityList = this.Context.GLLines.Where(x => x.HeaderId == companyId);
            return entityList;
        }

        public string Insert(GLLines entity)
        {
            this.Context.GLLines.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(GLLines entity)
        {
            var originalEntity = this.Context.GLLines.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.GLLines.Remove(this.GetSingle(id, companyId));
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
