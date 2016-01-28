using _360Accounting.Common;
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
            Account account = this.GetAll()
                .FirstOrDefault(x => x.SOBId == Convert.ToInt32(sobId) && 
                x.CompanyId == companyId);
            return account;
        }

        public Account GetSingle(string id)
        {
            Account account = this.GetAll()
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            IEnumerable<Account> accountList = this.Context.Accounts;
            return accountList;
        }

        public IEnumerable<AccountView> GetAll(long companyId, string searchText, bool paging, int page, string sort, string sortDir)
        {
            IEnumerable<Account> accountList = this.Context.Accounts;
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
            this.Context.Accounts.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id)
        {
            this.Context.Accounts.Remove(this.GetSingle(id));
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

        private AccountView GetAccountViewByAccountEntity(Account entity)
        {
            if (entity == null)
            {
                return null;
            }

            AccountView mappingObject = new AccountView();
            mappingObject.Id = entity.Id;
            mappingObject.SOBId = entity.SOBId;
            mappingObject.SOBName = this.Context.SetOfBooks.Where(x => x.Id == entity.SOBId).Select(x => x.Name).FirstOrDefault();
            mappingObject.Segments = Utility.Stringize("-", entity.SegmentName1, entity.SegmentName2, entity.SegmentName3, entity.SegmentName4, entity.SegmentName5, entity.SegmentName6, entity.SegmentName7, entity.SegmentName8);
            mappingObject.SegmentsLength = Utility.Stringize("-", entity.SegmentChar1, entity.SegmentChar2, entity.SegmentChar3, entity.SegmentChar4, entity.SegmentChar5, entity.SegmentChar6, entity.SegmentChar7, entity.SegmentChar8);
            return mappingObject;
        }
    }
}
