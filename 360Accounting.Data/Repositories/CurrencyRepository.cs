using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Data.Repositories
{
    public class CurrencyRepository : Repository, ICurrencyRepository
    {
        public IEnumerable<Currency> GetAll(long companyId, long sobId)
        {
            IEnumerable<Currency> currencyList = this.Context
                .Currencies.Where(x => x.CompanyId == companyId &&
                    x.SOBId == sobId);
            return currencyList;
        }

        public IEnumerable<Currency> GetAll(long companyId, long sobId, string searchText, bool paging, int page, string sort, string sortDir)
        {            
            IEnumerable<Currency> currencyList = this.Context.Currencies
                .Where(x => x.CompanyId == companyId &&
                    x.SOBId == sobId);
            currencyList = sortDir.ToUpper() == "ASC" ? currencyList.OrderBy(x => x.SOBId) : currencyList.OrderByDescending(x => x.SOBId);
            if (!paging)
            {
                return currencyList;
            }
            else
            {
                var recordCount = currencyList.Count();
                return currencyList.Skip((page - 1) * 20).Take(20);
            }
        }

        public Currency GetSingle(string id, long companyId)
        {
            Currency currency = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return currency;
        }

        public IEnumerable<Currency> GetAll(long companyId)
        {
            IEnumerable<Currency> currencyList = this.Context
                .Currencies.Where(x => x.CompanyId == companyId);
            return currencyList;
        }

        public string Insert(Currency entity)
        {
            this.Context.Currencies.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Currency entity)
        {
            var originalEntity = this.Context.Currencies.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Currencies.Remove(this.GetSingle(id, companyId));
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
