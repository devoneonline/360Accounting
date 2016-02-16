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
    public class JournalVoucherService : IJournalVoucherService
    {
        private IJournalVoucherRepository repository;

        public JournalVoucherService(IJournalVoucherRepository repo)
        {
            this.repository = repo;
        }

        public List<TrialBalance> TrialBalance(long companyId, long sobId, long fromCodeCombinationId, long toCodeCombinationId, long periodId)
        {
            return this.repository.TrialBalance(companyId, sobId, fromCodeCombinationId, toCodeCombinationId, periodId);
        }
        
        public List<Ledger> Ledger(long companyId, long sobId, long fromCodeCombinationId, long toCodeCombinationId, DateTime fromDate, DateTime toDate)
        {
            return this.repository.Ledger(companyId, sobId, fromCodeCombinationId, toCodeCombinationId, fromDate, toDate);
        }

        public List<AuditTrial> AuditTrial(long companyId, long sobId, DateTime fromDate, DateTime toDate)
        {
            return this.repository.AuditTrial(companyId, sobId, fromDate, toDate);
        }

        public List<UserwiseEntriesTrial> UserwiseEntriesTrial(long companyId, long sobId, DateTime fromDate, DateTime toDate, Guid? UserId)
        {
            return this.repository.UserwiseEntriesTrial(companyId, sobId, fromDate, toDate, UserId);
        }

        public IEnumerable<JournalVoucherDetail> GetAll(string headerId)
        {
            return this.repository.GetAll(headerId);
        }

        public string Insert(JournalVoucherDetail entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(JournalVoucherDetail entity)
        {
            return this.repository.Update(entity);
        }
        
        public IEnumerable<JournalVoucher> GetAll(long companyId, string searchText, bool paging, int? page, string sort, string sortDir)
        {
            return this.repository.GetAll(companyId, searchText, paging, page ?? 1, sort, sortDir);
        }

        public JournalVoucher GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<JournalVoucher> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(JournalVoucher entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(JournalVoucher entity)
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
    }
}
