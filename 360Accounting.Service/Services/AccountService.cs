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
    public class AccountService : IAccountService
    {
        private IAccountRepository repository;

        public AccountService(IAccountRepository repo)
        {
            this.repository = repo;
        }

        public Account GetAccountBySOBId(string sobId, long companyId)
        {
            return this.repository.GetAccountBySOBId(sobId, companyId);
        }

        public Account GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<Account> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public IEnumerable<AccountView> GetAll(long sobId, long companyId, string searchText, bool paging, int? page, string sort, string sortDir)
        {
            return this.repository.GetAll(sobId, companyId, searchText, paging, page ?? 1, sort, sortDir);
        }

        public string Insert(Account entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Account entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count( long companyId)
        {
            return this.repository.Count(companyId);
        }

        public long GetAccountIdBySegments(string segments, long companyId, long sobId)
        {
            return this.repository.GetAccountIdBySegments(segments, companyId, sobId);
        }

        public FeatureSetAccessList UserFeatureSet(long featureSetId, long companyId)
        {
            return this.repository.UserFeatureSet(featureSetId,companyId);
        }
    }
}
