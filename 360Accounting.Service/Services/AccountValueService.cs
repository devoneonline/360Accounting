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
    public class AccountValueService : IAccountValueService
    {
        private IAccountValueRepository repository;

        public AccountValueService(IAccountValueRepository repo)
        {
            this.repository = repo;
        }

        public List<AccountValue> GetAccountValuesByChartId(long chartId, long sobId)
        {
            return this.repository.GetAccountValuesByChartId(chartId, sobId);
        }

        public AccountValue GetAccountValueBySegment(long chartId, string segment)
        {
            return this.repository.GetAccountValueBySegment(chartId, segment);
        }

        public List<AccountValue> GetAccountValuesBySegment(long chartId, long sobId, string segment, int segmentNo, bool fetchSaved)
        {
            return this.repository.GetAccountValuesBySegment(chartId, sobId, segment, segmentNo, fetchSaved);
        }

        public List<AccountValue> GetAccountValuesBySegment(long chartId, long sobId, string segment)
        {
            return this.repository.GetAccountValuesBySegment(chartId, sobId, segment);
        }

        public AccountValue GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<AccountValue> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(AccountValue entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(AccountValue entity)
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
    }
}