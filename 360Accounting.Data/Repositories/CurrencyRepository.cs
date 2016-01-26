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
        public IEnumerable<Currency> GetAll(string searchText, bool paging, int page, string sort, string sortDir)
        {
            IEnumerable<Currency> currencyList =
                this.Context.Currencies;
            currencyList = sortDir.ToUpper() == "ASC" ?
                currencyList.OrderBy(x => x.SOBId) :
                currencyList.OrderByDescending(x => x.SOBId);
            if (!paging)
            {
                return currencyList.Select(x => new Currency
                {
                    CompanyId = x.CompanyId,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    CurrencyCode = x.CurrencyCode,
                    Name = x.Name,
                    Precision = x.Precision,
                    SOBId = x.SOBId,
                    SOBName = x.SOBName,
                    UpdateBy = x.UpdateBy,
                    UpdateDate = x.UpdateDate
                }).ToList();
            }
            else
            {
                var recordCount = currencyList.Count();
                return currencyList.Select(x => new Currency
                    {
                        CompanyId = x.CompanyId,
                        CreateBy = x.CreateBy,
                        CreateDate = x.CreateDate,
                        CurrencyCode = x.CurrencyCode,
                        Name = x.Name,
                        Precision = x.Precision,
                        SOBId = x.SOBId,
                        SOBName = x.SOBName,
                        UpdateBy = x.UpdateBy,
                        UpdateDate = x.UpdateDate
                    }).Skip((page - 1) * 20)
                    .Take(20);
            }
        }

        public Currency GetSingle(string id)
        {
            Currency currency = this.GetAll()
                .FirstOrDefault(x => x.CurrencyCode == id);
            return currency;
        }

        public IEnumerable<Currency> GetAll()
        {
            IEnumerable<Currency> currencyList = this.Context
                .Currencies;
            return currencyList;
        }

        public string Insert(Currency entity)
        {
            this.Context.Currencies.Add(entity);
            this.Commit();
            return entity.CurrencyCode;
        }

        public string Update(Currency entity)
        {
            this.Context.Currencies.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.CurrencyCode;
        }

        public void Delete(string id)
        {
            this.Context.Currencies.Remove(this.GetSingle(id));
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
