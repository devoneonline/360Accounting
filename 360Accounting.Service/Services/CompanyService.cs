using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _360Accounting.Service
{
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository repository;

        public CompanyService(ICompanyRepository repo)
        {
            this.repository = repo;
        }

        public Company GetSingle(string id)
        {
            return this.repository.GetSingle(id);
        }

        public IEnumerable<Company> GetAll()
        {
            return this.repository.GetAll();
        }

        public string Insert(Company entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Company entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id)
        {
            this.repository.Delete(id);
        }

        public int Count()
        {
            return this.repository.Count();
        }
    }
}
