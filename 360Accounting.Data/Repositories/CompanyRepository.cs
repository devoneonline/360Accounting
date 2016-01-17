using _360Accounting.Core.Entities;
using _360Accounting.Core.IRepository;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public string Update(Company entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}
