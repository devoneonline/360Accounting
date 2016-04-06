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

        public List<AccountValue> GetAccountValuesBySegment(long chartId, long sobId, string segment, int segmentNo, bool fetchSaved)
        {
            var query = this.Context.AccountValues.Where(x=> x.Segment == segment && x.ChartId == chartId);
            IEnumerable<string> segmentList = new List<string>();
            if (fetchSaved)
            {
                #region Preparing SegmentList

                switch (segmentNo)
	            {
                    case 1:
                        segmentList = this.Context.CodeCombinitions.Where(x => x.SOBId == sobId).Select(x => x.Segment1);
                        break;
                    case 2:
                        segmentList = this.Context.CodeCombinitions.Where(x => x.SOBId == sobId).Select(x=> x.Segment2);
                        break;
                    case 3:
                        segmentList = this.Context.CodeCombinitions.Where(x => x.SOBId == sobId).Select(x=> x.Segment3);
                        break;
                    case 4:
                        segmentList = this.Context.CodeCombinitions.Where(x => x.SOBId == sobId).Select(x=> x.Segment4);
                        break;
                    case 5:
                        segmentList = this.Context.CodeCombinitions.Where(x => x.SOBId == sobId).Select(x=> x.Segment5);
                        break;
                    case 6:
                        segmentList = this.Context.CodeCombinitions.Where(x => x.SOBId == sobId).Select(x=> x.Segment6);
                        break;
                    case 7:
                        segmentList = this.Context.CodeCombinitions.Where(x => x.SOBId == sobId).Select(x=> x.Segment7);
                        break;
                    case 8:
                        segmentList = this.Context.CodeCombinitions.Where(x => x.SOBId == sobId).Select(x=> x.Segment8);
                        break;

                }
                #endregion

                return query.Where(x => segmentList.Contains(x.Value)).ToList();
            }
            return query.ToList();
        }

        public List<AccountValue> GetAccountValuesBySegment(long chartId, long sobId, string segment)
        {
            var query = this.Context.AccountValues.Where(x => x.Segment == segment && x.ChartId == chartId);
            
            return query.ToList();
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
