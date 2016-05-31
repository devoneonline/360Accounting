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
    public class InvoiceRepository : Repository, IInvoiceRepository
    {
        public Invoice GetSingle(long companyId, long sobId, long periodId, long currencyId)
        {
            Invoice invoice =  this.GetAll(companyId).Where(rec => rec.SOBId == sobId && rec.PeriodId == periodId && rec.CurrencyId == currencyId).OrderByDescending(rec => rec.Id).FirstOrDefault();
            return invoice;
        }

        public IEnumerable<InvoiceView> GetAll(long companyId, long sobId)
        {
            var query = (from a in this.Context.Invoices
                        join b in this.Context.ReceivablePeriods on a.PeriodId equals b.Id
                        join c in this.Context.Currencies on a.CurrencyId equals c.Id
                        join d in this.Context.Calendars on b.CalendarId equals d.Id
                        where a.CompanyId == companyId && a.SOBId == sobId

                        select new InvoiceView
                        {
                            CompanyId = a.CompanyId,
                            ConversionRate = a.ConversionRate,
                            CreateBy = a.CreateBy,
                            CreateDate = a.CreateDate,
                            CurrencyId = a.CurrencyId,
                            CurrencyName = c.Name,
                            CustomerId = a.CustomerId,
                            CustomerSiteId = a.CustomerSiteId,
                            Id = a.Id,
                            InvoiceDate = a.InvoiceDate,
                            InvoiceNo = a.InvoiceNo,
                            InvoiceType = a.InvoiceType,
                            PeriodId = a.PeriodId,
                            PeriodName = d.PeriodName,
                            Remarks = a.Remarks,
                            SOBId = a.SOBId,
                            UpdateBy = a.UpdateBy,
                            UpdateDate = a.UpdateDate
                        }).ToList();
            //IEnumerable<Invoice> list = this.Context.Invoices.Where(x => x.SOBId == sobId && 
            //        x.PeriodId == periodId &&
            //        x.CurrencyId == currencyId).ToList();
            return query;
        }

        public IEnumerable<Invoice> GetInvoices(long companyId, long sobId, long periodId)
        {
            IEnumerable<Invoice> list = this.Context.Invoices.Where(x => x.SOBId == sobId &&
                    x.PeriodId == periodId).ToList();
            return list;
        }

        public Invoice GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            Invoice invoice = this.Context.Invoices.FirstOrDefault(a => a.CompanyId == companyId && a.Id == longId);
            return invoice;
        }

        public IEnumerable<Invoice> GetAll(long companyId)
        {
            IEnumerable<Invoice> list = this.Context.Invoices.Where(x => x.CompanyId == companyId);
            return list;
        }

        public string Insert(Invoice entity)
        {
            this.Context.Invoices.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Invoice entity)
        {
            Invoice originalEntity = this.Context.Invoices.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Invoices.Remove(this.GetSingle(id, companyId));
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

        public IEnumerable<Invoice> GetByCurrencyId(long companyId, long sobId, long currencyId)
        {
            return this.Context.Invoices.Where(rec => rec.CompanyId == companyId && rec.SOBId == sobId && rec.CurrencyId == currencyId);
        }




        public List<CustomerSales> CustomerSales(long companyId, long sobId, DateTime fromDate, DateTime toDate, long customerId)
        {
            var data = (from a in this.Context.Invoices
                        join b in this.Context.InvoiceDetails on a.Id equals b.InvoiceId
                        join c in this.Context.Customers on a.CustomerId equals c.Id
                        join d in this.Context.Items on b.ItemId equals d.Id into e
                        from f in e.DefaultIfEmpty()
                        join g in this.Context.InvoiceSources on b.InvoiceSourceId equals g.Id into h
                        from i in h.DefaultIfEmpty()
                        where a.CompanyId == companyId && a.SOBId == sobId &&
                        a.InvoiceDate >= fromDate && a.InvoiceDate <= toDate
                        select new CustomerSales
                        {
                            Amount = b.Quantity * b.Rate,
                            CustomerId = a.CustomerId,
                            CustomerName = c.CustomerName,                            
                            InvoiceSourceName = i.Description,
                            ItemName = f.ItemName,
                            Quantity = b.Quantity,
                            TaxAmount = 0,
                            TotalAmount = (b.Quantity * b.Rate) + 0
                        }).ToList();

            if (customerId != 0)
                data = data.Where(x => x.CustomerId == customerId).ToList();

            return data;
        }

        public List<InvoiceAuditTrail> InvoiceAuditTrail(long companyId, long sobId, DateTime fromDate, DateTime toDate)
        {
            var data = (from a in this.Context.Invoices
                        join b in this.Context.InvoiceDetails on a.Id equals b.InvoiceId
                        join c in this.Context.Customers on a.CustomerId equals c.Id
                        join d in this.Context.CustomerSites on a.CustomerSiteId equals d.Id
                        join e in this.Context.Items on b.ItemId equals e.Id into f
                        from g in f.DefaultIfEmpty()
                        join h in this.Context.InvoiceSources on b.InvoiceSourceId equals h.Id into i
                        from j in i.DefaultIfEmpty()
                        join k in this.Context.Taxes on b.TaxId equals k.Id into l
                        from m in l.DefaultIfEmpty()
                        where a.CompanyId == companyId && a.SOBId == sobId &&
                        a.InvoiceDate >= fromDate && a.InvoiceDate <= toDate
                        select new InvoiceAuditTrail
                        {
                            Amount = b.Quantity * b.Rate,
                            CustomerName = c.CustomerName,
                            CustomerSiteName = d.SiteName,
                            InvoiceDate = a.InvoiceDate,
                            InvoiceNo = a.InvoiceNo,
                            ItemName = b.ItemId == null ? j.Description : g.ItemName,
                            Quantity = b.Quantity,
                            Rate = b.Rate,
                            TaxAmount = 0,
                            TaxName = m.TaxName,
                            TotalAmount = (b.Quantity * b.Rate) + 0,
                            UOM = ""
                        }).ToList();

            return data;
        }

        public List<InvoicePrintout> InvoicePrintout(long companyId, long sobId, DateTime fromDate, DateTime toDate, string invoiceNo, long customerId, long customerSiteId)
        {
            var data = (from a in this.Context.Invoices
                        join b in this.Context.InvoiceDetails on a.Id equals b.InvoiceId
                        join c in this.Context.Customers on a.CustomerId equals c.Id
                        join d in this.Context.CustomerSites on a.CustomerSiteId equals d.Id
                        join e in this.Context.Items on b.ItemId equals e.Id into f
                        from g in f.DefaultIfEmpty()
                        join h in this.Context.InvoiceSources on b.InvoiceSourceId equals h.Id into i
                        from j in i.DefaultIfEmpty()
                        where a.CompanyId == companyId && a.SOBId == sobId &&
                        a.InvoiceDate >= fromDate && a.InvoiceDate <= toDate
                        select new InvoicePrintout
                        {
                            Amount = b.Quantity * b.Rate,
                            CustomerId = a.CustomerId,
                            CustomerName = c.CustomerName,
                            CustomerSiteId = a.CustomerSiteId,
                            CustomerSiteName = d.SiteName,
                            InvoiceDate = a.InvoiceDate,
                            InvoiceNo = a.InvoiceNo,
                            ItemName = b.ItemId == null ? j.Description : g.ItemName,
                            OrderReferenceNo = "",
                            Quantity = b.Quantity,
                            Rate = b.Rate,
                            Remarks = a.Remarks,
                            SalesTaxVAT = 0,
                            UOM = ""
                        }).ToList();

            if (customerId != 0)
                data = data.Where(x => x.CustomerId == customerId).ToList();

            if (customerSiteId != 0)
                data = data.Where(x => x.CustomerSiteId == customerSiteId).ToList();

            if (invoiceNo != "" && invoiceNo != null)
                data = data.Where(x => x.InvoiceNo == invoiceNo).ToList();

            return data;
        }
    }
}
