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
        public AccountValue GetAccountValueBySegment(long chartId, string segment)
        {
            AccountValue value = this.Context.AccountValues.FirstOrDefault(x => x.ChartId == chartId && x.Segment == segment);
            return value;
        }

        public List<AccountValue> GetAccountValuesBySegment(long chartId, string segment)
        {
            List<AccountValue> valueList = this.Context.AccountValues.Where(x => x.Segment == segment && x.ChartId == chartId).ToList();
            return valueList;
        }

        public AccountValue GetSingle(string id, long companyId)
        {
            long longId=Convert.ToInt64(id);
            AccountValue entity =
                (from a in this.Context.AccountValues
                 join b in this.Context.Accounts on a.ChartId equals b.Id
                 where a.Id == longId && b.CompanyId == companyId
                 select a).FirstOrDefault();                
            return entity;
        }

        public IEnumerable<AccountValue> GetAll(long companyId)
        {
            IEnumerable<AccountValue> valueList = from a in this.Context.AccountValues
                                                  join b in this.Context.Accounts on a.ChartId equals b.Id
                                                  where b.CompanyId == companyId
                                                  select a;

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
            AccountValue originalEntity = this.Context.AccountValues.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.AccountValues.Remove(this.GetSingle(id, companyId));
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
