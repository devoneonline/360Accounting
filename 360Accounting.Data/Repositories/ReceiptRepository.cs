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
            {
                date = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day);
                query = query.Where(x => x.ReceiptDate == date);
            }
                

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
                        where a.CompanyId == companyId
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

        public Receipt GetSingle(long companyId, long sobId, long periodId, long currencyId, long customerId)
        {
            Receipt entity = this.GetAll(companyId)
                .Where(x => x.PeriodId == periodId && x.SOBId == sobId &&
                    x.CurrencyId == currencyId && x.CustomerId == customerId).
                    OrderByDescending(rec => rec.Id).FirstOrDefault();
            return entity;
        }

        public IEnumerable<Receipt> GetByCurrencyId(long companyId, long sobId, long currencyId)
        {
            return this.Context.Receipts.Where(rec => rec.CompanyId == companyId && rec.SOBId == sobId && rec.CurrencyId == currencyId);
        }



        public List<ReceiptAuditTrial> ReceiptAuditTrial(long companyId, long sobId, DateTime fromDate, DateTime toDate)
        {
            var data = (from a in this.Context.Receipts
                        join b in this.Context.BankAccounts on a.BankAccountId equals b.Id
                        join c in this.Context.Banks on a.BankId equals c.Id
                        join d in this.Context.Customers on a.CustomerId equals d.Id
                        join e in this.Context.CustomerSites on a.CustomerSiteId equals e.Id
                        where a.CompanyId == companyId && a.SOBId == sobId &&
                        a.ReceiptDate >= fromDate && a.ReceiptDate <= toDate
                        select new ReceiptAuditTrial
                        {
                            Amount = a.ReceiptAmount,
                            BankAccountName = b.AccountName,
                            BankName = c.BankName,
                            CustomerName = d.CustomerName,
                            CustomerSiteName = e.SiteName,
                            ReceiptDate = a.ReceiptDate,
                            ReceiptNo = a.ReceiptNumber,
                            Status = a.Status
                        }).ToList();

            return data;
        }

        public List<ReceiptPrintout> ReceiptPrintout(long companyId, long sobId, DateTime fromDate, DateTime toDate, string receiptNo, long customerId, long customerSiteId)
        {
            var data = (from a in this.Context.Receipts
                        join b in this.Context.Customers on a.CustomerId equals b.Id
                        where a.CompanyId == companyId && a.SOBId == sobId &&
                        a.ReceiptDate >= fromDate && a.ReceiptDate <= toDate
                        select new ReceiptPrintout
                        {
                            CustomerId = a.CustomerId,
                            CustomerName = b.CustomerName,
                            CustomerSiteId = a.CustomerSiteId,
                            ReceiptAmount = a.ReceiptAmount,
                            ReceiptNo = a.ReceiptNumber,
                            Remarks = a.Remarks                            
                        }).ToList();

            if (receiptNo != "" && receiptNo != null)
                data = data.Where(a => a.ReceiptNo == receiptNo).ToList();

            if (customerId != 0)
                data = data.Where(x => x.CustomerId == customerId).ToList();

            if (customerSiteId != 0)
                data = data.Where(x => x.CustomerSiteId == customerSiteId).ToList();

            return data;
        }
        
        public List<CustomerwiseReceiptClearance> CustomerwiseReceiptClearance(long companyId, long sobId, DateTime fromDate, DateTime toDate, long customerId)
        {
            var data = (from a in this.Context.Receipts
                        join b in this.Context.Customers on a.CustomerId equals b.Id
                        join c in this.Context.Banks on a.BankId equals c.Id
                        join d in this.Context.BankAccounts on a.BankAccountId equals d.Id
                        where a.CompanyId == companyId && a.SOBId == sobId &&
                        a.ReceiptDate >= fromDate && a.ReceiptDate <= toDate
                        //&& a.Status.ToUpper() == "CLEAR"
                        select new CustomerwiseReceiptClearance
                        {
                            Amount = a.ReceiptAmount,
                            BankAccountName = d.AccountName,
                            BankName = c.BankName,
                            CustomerId = a.CustomerId,
                            CustomerName = b.CustomerName,
                            ReceiptNo = a.ReceiptNumber
                        }).ToList();

            if (customerId != 0)
                data = data.Where(x => x.CustomerId == customerId).ToList();

            return data;
        }
    }
}
