using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace _360Accounting.Data.Repositories
{
    public class CompanyRepository : Repository, ICompanyRepository
    {
        public Company GetSingle(string id)
        {
            return this.GetAll().FirstOrDefault(x => x.Id == Convert.ToInt32(id));
        }

        public IEnumerable<Company> GetAll()
        {
            return this.Context.Companies;
        }

        public string Insert(Company entity)
        {
            this.Context.Companies.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Company entity)
        {
            this.Context.Companies.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id)
        {
            this.Context.Companies.Remove(this.GetSingle(id));
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
