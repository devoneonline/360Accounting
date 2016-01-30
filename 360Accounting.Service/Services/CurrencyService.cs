using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class CurrencyService : ICurrencyService
    {
        private ICurrencyRepository repository;

        public CurrencyService(ICurrencyRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<Currency> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public IEnumerable<Currency> GetAll(long companyId, long sobId, string searchText, bool paging, int? page, string sort, string sortDir)
        {
            return this.repository.GetAll(companyId, sobId, searchText, paging, page ?? 1, sort, sortDir);
        }

        public Currency GetSingle(string id)
        {
            return this.repository.GetSingle(id);
        }

        public IEnumerable<Currency> GetAll()
        {
            return this.repository.GetAll();
        }

        public string Insert(Currency entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Currency entity)
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
