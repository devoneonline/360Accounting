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
    public class GLHeaderRepository:Repository, IGLHeaderRepository
    {

        public GLHeader GetSingle(string id, long companyId)
        {
            GLHeader entity = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return entity;
        }

        public IEnumerable<GLHeader> GetAll(long companyId)
        {
            IEnumerable<GLHeader> entityList = this.Context.GLHeaders.Where(x => x.CompanyId == companyId);
            return entityList;
        }

        public string Insert(GLHeader entity)
        {
            this.Context.GLHeaders.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(GLHeader entity)
        {
            var originalEntity = this.Context.GLHeaders.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.GLHeaders.Remove(this.GetSingle(id, companyId));
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
