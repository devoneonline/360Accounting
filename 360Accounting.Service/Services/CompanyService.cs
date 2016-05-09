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

        public Company GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<Company> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Company entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Company entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }

        public IEnumerable<Company> GetAll(long companyId, string userRole)
        {
            return this.repository.GetAll(companyId, userRole);
        }
    }
}
