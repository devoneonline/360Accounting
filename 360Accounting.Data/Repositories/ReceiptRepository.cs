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
    public class ReceiptRepository : Repository, IReceiptRepository
    {
        public IEnumerable<ReceiptView> GetReceipts(long sobId, long bankId, long bankAccountId, DateTime? date = null)
        {
            var query = from a in this.Context.Receipts
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join d in this.Context.Customers on a.CustomerId equals d.Id
                        join e in this.Context.Currencies on a.CurrencyId equals e.Id
                        join f in this.Context.Banks on a.BankId equals f.Id
                        join g in this.Context.BankAccounts on a.BankAccountId equals g.Id
                        join h in this.Context.CustomerSites on a.CustomerSiteId equals h.Id
                        where a.SOBId == sobId
                        && a.BankId == bankId 
                        && a.BankAccountId == bankAccountId                         
                        select new ReceiptView
                        {
                            CustomerName = d.CustomerName,
                            BankAccountId = a.BankAccountId,
                            Status = a.Status,
                            SOBId = a.SOBId,
                            BankAccountName = g.AccountName,
                            Remarks = a.Remarks,
                            ReceiptNumber = a.ReceiptNumber,
                            ReceiptAmount = a.ReceiptAmount,
                            ReceiptDate = a.ReceiptDate,
                            Id = a.Id,
                            BankId = a.BankId,
                            BankName = f.BankName,
                            ConversionRate = a.ConversionRate,
                            CurrencyId = a.CurrencyId,
                            CustomerId = a.CustomerId,
                            CustomerSiteId = a.CustomerSiteId,
                            CustomerSiteName = h.SiteName,
                            PeriodId = a.PeriodId
                        };

            if (date != null)
                query = query.Where(x => x.ReceiptDate == date);

            return query;
        }

        public IEnumerable<ReceiptView> GetReceipts(long sobId, long periodId, long customerId, long currencyId, long companyId)
        {
            var query = from a in this.Context.Receipts
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join d in this.Context.Customers on a.CustomerId equals d.Id
                        join e in this.Context.Currencies on a.CurrencyId equals e.Id
                        join f in this.Context.Banks on a.BankId equals f.Id
                        join g in this.Context.BankAccounts on a.BankAccountId equals g.Id
                        join h in this.Context.CustomerSites on a.CustomerSiteId equals h.Id
                        where a.SOBId == sobId && a.PeriodId == periodId && a.CustomerId == customerId && 
                        a.CurrencyId == currencyId && b.CompanyId == companyId && d.Id == customerId &&
                        e.Id == currencyId
                        select new ReceiptView
                        {
                            BankAccountId = a.BankAccountId,
                            Status = a.Status,
                            SOBId = a.SOBId,
                            BankAccountName = g.AccountName,
                            Remarks = a.Remarks,
                            ReceiptNumber = a.ReceiptNumber,
                            ReceiptAmount = a.ReceiptAmount,
                            ReceiptDate = a.ReceiptDate,
                            Id = a.Id,
                            BankId = a.BankId,
                            BankName = f.BankName,
                            ConversionRate = a.ConversionRate,
                            CurrencyId = a.CurrencyId,
                            CustomerId = a.CustomerId,
                            CustomerSiteId = a.CustomerSiteId,
                            CustomerSiteName = h.SiteName,
                            PeriodId = a.PeriodId
                        };
            return query;
        }

        public Receipt GetSingle(string id, long companyId)
        {
            Receipt receipt = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return receipt;
        }

        public IEnumerable<Receipt> GetAll(long companyId)
        {
            var query = from a in this.Context.Receipts
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        where c.Id == companyId
                        select a;
            return query;
        }

        public string Insert(Receipt entity)
        {
            this.Context.Receipts.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Receipt entity)
        {
            var originalEntity = this.Context.Receipts.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Receipts.Remove(this.GetSingle(id, companyId));
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
