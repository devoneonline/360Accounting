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
    public class AccountValueRepository : Repository, IAccountValueRepository
    {
        public AccountValue GetAccountValueBySegment(string segment, long chartId)
        {
            AccountValue value = this.Context
                .AccountValues.FirstOrDefault(x => x.ChartId == chartId &&
                    x.Segment == segment);
            return value;
        }

        public List<AccountValue> GetAccountValuesBySegment(string segment, long chartId)
        {
            List<AccountValue> valueList = this.Context
                .AccountValues.Where(x => x.Segment == segment &&
                    x.ChartId == chartId).ToList();
            return valueList;
        }

        public AccountValue GetSingle(string id)
        {
            AccountValue accountValue = this.GetAll()
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return accountValue;
        }

        public IEnumerable<AccountValue> GetAll()
        {
            IEnumerable<AccountValue> valueList = this.Context
                .AccountValues;
            return valueList;
        }

        public string Insert(AccountValue entity)
        {
            this.Context.AccountValues.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(AccountValue entity)
        {
            this.Context.AccountValues.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id)
        {
            this.Context.AccountValues.Remove(this.GetSingle(id));
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
