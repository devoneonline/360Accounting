using _360Accounting.Common;
using _360Accounting.Core;
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
    public class AccountRepository : Repository, IAccountRepository
    {
        public Account GetAccountBySOBId(string sobId, long companyId)
        {
            long longId = Convert.ToInt32(sobId);
            Account account = this.Context.Accounts.FirstOrDefault(x => x.SOBId == longId && x.CompanyId == companyId);
            return account;
        }

        public Account GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt32(id);
            Account account = this.Context.Accounts.FirstOrDefault(x => x.CompanyId == companyId && x.Id == longId);
            return account;
        }

        public IEnumerable<Account> GetAll(long companyId)
        {
            IEnumerable<Account> accountList = this.Context.Accounts.Where(x => x.CompanyId == companyId);
            return accountList;
        }

        public IEnumerable<AccountView> GetAll(long sobId, long companyId, string searchText, bool paging, int page, string sort, string sortDir)
        {
            IEnumerable<Account> accountList = this.Context.Accounts.Where(rec => rec.CompanyId == companyId && rec.SOBId == sobId);
            accountList = sortDir.ToUpper() == "ASC" ?
                accountList.OrderBy(x => x.SOBId) :
                accountList.OrderByDescending(x => x.SOBId);

            if (!paging)
            {
                return accountList.Select(x => GetAccountViewByAccountEntity(x)).ToList();
            }
            else
            {
                var recordCount = accountList.Count();
                return accountList
                    .Select(x => GetAccountViewByAccountEntity(x))
                    .Skip((page - 1) * 20)
                    .Take(20);
            }
        }

        public string Insert(Account entity)
        {
            this.Context.Accounts.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Account entity)
        {
            var originalEntity = this.Context.Accounts.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Accounts.Remove(this.GetSingle(id, companyId));
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

        private AccountView GetAccountViewByAccountEntity(Account entity)
        {
            if (entity == null) return null;

            AccountView mappingObject = new AccountView();
            mappingObject.Id = entity.Id;
            mappingObject.SOBId = entity.SOBId;
            mappingObject.SOBName = this.Context.SetOfBooks.Where(x => x.Id == entity.SOBId).Select(x => x.Name).FirstOrDefault();
            mappingObject.Segments = Utility.Stringize("-", 
                Convert.ToBoolean(entity.SegmentEnabled1) ? entity.SegmentName1 : "",
                Convert.ToBoolean(entity.SegmentEnabled2) ? entity.SegmentName2 : "", 
                Convert.ToBoolean(entity.SegmentEnabled3) ? entity.SegmentName3 : "", 
                Convert.ToBoolean(entity.SegmentEnabled4) ? entity.SegmentName4 : "", 
                Convert.ToBoolean(entity.SegmentEnabled5) ? entity.SegmentName5 : "", 
                Convert.ToBoolean(entity.SegmentEnabled6) ? entity.SegmentName6 : "", 
                Convert.ToBoolean(entity.SegmentEnabled7) ? entity.SegmentName7 : "",
                Convert.ToBoolean(entity.SegmentEnabled8) ? entity.SegmentName8 : "");
            mappingObject.SegmentsLength = Utility.Stringize("-", 
                Convert.ToBoolean(entity.SegmentEnabled1) ? entity.SegmentChar1 : (int?)null,
                Convert.ToBoolean(entity.SegmentEnabled2) ? entity.SegmentChar2 : (int?)null,
                Convert.ToBoolean(entity.SegmentEnabled3) ? entity.SegmentChar3 : (int?)null,
                Convert.ToBoolean(entity.SegmentEnabled4) ? entity.SegmentChar4 : (int?)null,
                Convert.ToBoolean(entity.SegmentEnabled5) ? entity.SegmentChar5 : (int?)null,
                Convert.ToBoolean(entity.SegmentEnabled6) ? entity.SegmentChar6 : (int?)null,
                Convert.ToBoolean(entity.SegmentEnabled7) ? entity.SegmentChar7 : (int?)null,
                Convert.ToBoolean(entity.SegmentEnabled8) ? entity.SegmentChar8 : (int?)null);
            return mappingObject;
        }

        public long GetAccountIdBySegments(string segments, long companyId, long sobId)
        {
            var segmentList = segments.Split(new char[] {'.'}, StringSplitOptions.None);
            var query = this.Context.CodeCombinitions.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.AllowedPosting == true);

            for(int i=0;i<segmentList.Count();i++)
            {
                switch (i)
                {
                    case 0:
                        string segment1=segmentList[i];
                        query = query.Where(x => x.Segment1 ==segment1);
                        break;
                    case 1:
                        string segment2=segmentList[i];
                        query = query.Where(x => x.Segment2 == segment2);
                        break;
                    case 2:
                        string segment3=segmentList[i];
                        query = query.Where(x => x.Segment3 == segment3);
                        break;
                    case 3:
                        string segment4=segmentList[i];
                        query = query.Where(x => x.Segment4 == segment4);
                        break;
                    case 4:
                        string segment5=segmentList[i];
                        query = query.Where(x => x.Segment5 == segment5);
                        break;
                    case 5:
                        string segment6=segmentList[i];
                        query = query.Where(x => x.Segment6 == segment6);
                        break;
                    case 6:
                        string segment7=segmentList[i];
                        query = query.Where(x => x.Segment7 == segment7);
                        break;
                    case 7:
                        string segment8=segmentList[i];
                        query = query.Where(x => x.Segment8 == segment8);
                        break;
                }
            }
            CodeCombinition entity = query.FirstOrDefault();
            if (entity != null)
            {
                return entity.Id;
            }
            else
                return 0;
        }

        public FeatureSetAccessList UserFeatureSet(long featureSetId, long companyId)
        {
            FeatureSetAccessList entity = new FeatureSetAccessList();
            entity.FeatureSetId = featureSetId;
            entity.SelectedUser = (from a in this.Context.FeatureSetAccesses
                                   join b in this.Context.Users on a.UserId equals b.UserId
                                   where a.FeatureSetId == featureSetId && a.CompanyId == companyId
                                   select new
                                   {
                                       b.UserId,
                                       b.Username
                                   }
                                ).ToDictionary(x => x.UserId, x => x.Username);
            return entity;
        }
    }
}
