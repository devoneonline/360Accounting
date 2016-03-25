using _360Accounting.Core.IRepository;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace _360Accounting.Data.Repositories
{
    public class PaymentRepository : Repository, IPaymentRepository
    {
        public PaymentHeader GetSingle(string id, long companyId)
        {
            long longId=Convert.ToInt64(id);

            var query = (from a in this.Context.PaymentHeaders
                         join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                         join c in this.Context.Companies on b.CompanyId equals c.Id
                         where b.CompanyId == companyId && a.Id == longId
                         select a).FirstOrDefault();
            return query;
        }

        public PaymentHeader GetSingle(long companyId, long vendorId, long bankId, long sobId, long periodId)
        {
            var query = (from a in this.Context.PaymentHeaders
                         join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                         join c in this.Context.Companies on b.CompanyId equals c.Id
                         where b.CompanyId == companyId && a.VendorId == vendorId && a.BankId == bankId && a.SOBId == sobId && a.PeriodId == periodId
                         select a).OrderByDescending(rec => rec.Id).FirstOrDefault();
            return query;
        }

        public IEnumerable<PaymentHeader> GetAll(long companyId)
        {
            var query = from a in this.Context.PaymentHeaders
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        where b.CompanyId == companyId
                        select a;
            return query;
        }

        public IEnumerable<PaymentHeaderView> GetAll(long companyId, long vendorId, long bankId, long sobId, long periodId)
        {
            var query = from a in this.Context.PaymentHeaders
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        join d in this.Context.BankAccounts on a.BankAccountId equals d.Id
                        join e in this.Context.VendorSites on a.VendorSiteId equals e.Id
                        join f in this.Context.Banks on a.BankId equals f.Id
                        join g in this.Context.Vendors on a.VendorId equals g.Id
                        join h in this.Context.Calendars on a.PeriodId equals h.Id
                        where b.CompanyId == companyId && a.VendorId == vendorId && a.BankId == bankId && a.SOBId == sobId && periodId == a.PeriodId
                        select new PaymentHeaderView
                        {
                            Amount = a.Amount,
                            BankAccountName = d.AccountName,
                            VendorSiteName = e.Name,
                            BankAccountId = a.BankAccountId,
                            BankId = a.BankId,
                            CreateBy = a.CreateBy,
                            CreateDate = a.CreateDate,
                            Id = a.Id,
                            PaymentDate = a.PaymentDate,
                            PaymentNo = a.PaymentNo,
                            PeriodId = a.PeriodId,
                            SOBId = a.SOBId,
                            Status = a.Status,
                            UpdateBy = a.UpdateBy,
                            UpdateDate = a.UpdateDate,
                            VendorId = a.VendorId,
                            VendorSiteId = a.VendorSiteId
                        };
            return query;
        }

        public string Insert(PaymentHeader entity)
        {
            this.Context.PaymentHeaders.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(PaymentHeader entity)
        {
            var originalEntity = this.Context.PaymentHeaders.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.PaymentHeaders.Remove(this.GetSingle(id, companyId));
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

        public IEnumerable<PaymentInvoiceLines> GetAllLines(long paymentId, long companyId)
        {
            var query = from a in this.Context.PaymentInvoiceLines
                        join b in this.Context.PaymentHeaders on a.PaymentId equals b.Id
                        join c in this.Context.SetOfBooks on b.SOBId equals c.Id
                        join d in this.Context.Companies on c.CompanyId equals d.Id
                        where a.PaymentId == paymentId && c.CompanyId == companyId
                        select a;
            return query.ToList();
        }

        public long Insert(PaymentInvoiceLines entity)
        {
            this.Context.PaymentInvoiceLines.Add(entity);
            this.Commit();
            return entity.Id;
        }

        public long Update(PaymentInvoiceLines entity)
        {
            var originalEntity = this.Context.PaymentInvoiceLines.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id;
        }

        public void DeleteLine(long id, long companyId)
        {
            this.Context.PaymentInvoiceLines.Remove(this.Context.PaymentInvoiceLines.Find(id));
            this.Commit();
        }

        public PaymentInvoiceLines GetSingle(long id)
        {
            return this.Context.PaymentInvoiceLines.FirstOrDefault(x => x.Id == id);
        }
    }
}
