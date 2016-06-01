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
    public class ReceiptService : IReceiptService
    {
        private IReceiptRepository repository;

        public ReceiptService(IReceiptRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<ReceiptView> GetReceipts(long sobId, long bankId, long bankAccountId, DateTime? date = null)
        {
            return this.repository.GetReceipts(sobId, bankId, bankAccountId, date);
        }
        
        public IEnumerable<ReceiptView> GetReceipts(long sobId, long periodId, long customerId, long currencyId, long companyId)
        {
            return this.repository.GetReceipts(sobId, periodId, customerId, currencyId, companyId);
        }

        public Receipt GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<Receipt> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Receipt entity)
        {
            if (entity.IsValid())
                return this.repository.Insert(entity);
            else
                return "Entity is not in valid state";
        }

        public string Update(Receipt entity)
        {
            if (entity.IsValid())
                return this.repository.Update(entity);
            else
                return "Entity is not in valid state";
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count( long companyId)
        {
            return this.repository.Count(companyId);
        }

        public Receipt GetSingle(long companyId, long sobId, long periodId, long currencyId, long customerId)
        {
            return this.repository.GetSingle(companyId, sobId, periodId, currencyId, customerId);
        }

        public IEnumerable<Receipt> GetByCurrencyId(long companyId, long sobId, long currencyId)
        {
            return this.GetByCurrencyId(companyId, sobId, currencyId);
        }




        public List<ReceiptAuditTrial> ReceiptAuditTrial(long companyId, long sobId, DateTime fromDate, DateTime toDate)
        {
            return this.repository.ReceiptAuditTrial(companyId, sobId, fromDate, toDate);
        }


        public List<ReceiptPrintout> ReceiptPrintout(long companyId, long sobId, DateTime fromDate, DateTime toDate, string receiptNo, long customerId, long customerSiteId)
        {
            return this.repository.ReceiptPrintout(companyId, sobId, fromDate, toDate, receiptNo, customerId, customerSiteId);
        }
    }
}
