using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using _360Accounting.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace _360Accounting.Data.Repositories
{
    public class CompanyRepository : Repository, ICompanyRepository
    {
        public Company GetSingle(string id, long companyId)
        {
            return this.GetAll(companyId).FirstOrDefault(x => x.Id == Convert.ToInt32(id));
        }

        public IEnumerable<Company> GetAll(long companyId)
        {
            return this.Context.Companies;
        }

        public IEnumerable<Company> GetAll(long companyId, string userRole)
        {
            if (userRole.ToUpper() == UserRoles.SuperAdmin.ToString().ToUpper())
                return GetAll(companyId);
            else
            {
                return this.Context.Companies.Where(x => x.Id == companyId);
            }
        }

        public string Insert(Company entity)
        {
            this.Context.Companies.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Company entity)
        {
            var originalEntity = this.Context.Companies.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Companies.Remove(this.GetSingle(id, companyId));
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
