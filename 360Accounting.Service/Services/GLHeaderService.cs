using _360Accounting.Core;
using _360Accounting.Core.Interfaces;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class GLHeaderService : IGLHeaderService
    {
        private IGLHeaderRepository repository;

        public GLHeaderService(IGLHeaderRepository repo)
        {
            this.repository = repo;
        }

        public GLHeader GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public GLHeader GetSingle(long companyId, long periodId, long sobId, long currencyId)
        {
            return this.repository.GetSingle(companyId, periodId, sobId, currencyId);
        }

        public IEnumerable<GLHeaderView> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public IEnumerable<GLHeader> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(GLHeader entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(GLHeader entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id, companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }

        public List<TrialBalance> TrialBalance(long companyId, long sobId, long fromCodeCombinationId, long toCodeCombinationId, long periodId)
        {
            return this.repository.TrialBalance(companyId, sobId, fromCodeCombinationId, toCodeCombinationId, periodId);
        }

        public List<Ledger> Ledger(long companyId, long sobId, long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate)
        {
            return this.repository.Ledger(companyId, sobId, fromCodeCombinationId, toCodeCombinationId, fromDate, toDate);
        }

        public List<AuditTrail> AuditTrail(long companyId, long sobId, DateTime fromDate, DateTime toDate)
        {
            return this.repository.AuditTrail(companyId, sobId, fromDate, toDate);
        }

        public List<UserwiseEntriesTrail> UserwiseEntriesTrail(long companyId, long sobId, DateTime fromDate, DateTime toDate, Guid? UserId)
        {
            return this.repository.UserwiseEntriesTrail(companyId, sobId, fromDate, toDate, UserId);
        }

        public IEnumerable<GLHeader> GetByCurrencyId(long companyId, long sobId, long currencyId)
        {
            return this.GetByCurrencyId(companyId, sobId, currencyId);
        }
    }
}
