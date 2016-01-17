using _360Accounting.Core.Entities;
using _360Accounting.Core.IRepository;
using _360Accounting.Core.IService;
using System;
using System.Collections.Generic;

namespace _360Accounting.Service.Services
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
            throw new NotImplementedException();
        }

        public IEnumerable<Company> GetAll()
        {
            return this.repository.GetAll();
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
