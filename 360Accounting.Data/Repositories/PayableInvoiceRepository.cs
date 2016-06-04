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
    public class PayableInvoiceRepository : Repository, IPayableInvoiceRepository
    {
        public PayableInvoice GetSingle(long companyId, long sobId, long periodId)
        {
            IEnumerable<PayableInvoice> entityList = this.GetAll(companyId);
            if (entityList.Count() > 0)
            {
                PayableInvoice entity = entityList.Where(x => x.SOBId == sobId && x.PeriodId == periodId)
                    .OrderByDescending(rec => rec.Id).First();
                return entity;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<PayableInvoice> GetAll(long companyId, long sobId)
        {
            IEnumerable<PayableInvoice> list = this.GetAll(companyId).Where(x => x.SOBId == sobId);
            return list;
        }

        public PayableInvoice GetSingle(string id, long companyId)
        {
            PayableInvoice invoice = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return invoice;
        }

        public IEnumerable<PayableInvoice> GetAll(long companyId)
        {
            IEnumerable<PayableInvoice> list = this.Context.PayableInvoices;
            return list;
        }

        public string Insert(PayableInvoice entity)
        {
            this.Context.PayableInvoices.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(PayableInvoice entity)
        {
            PayableInvoice originalEntity = this.Context.PayableInvoices.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.PayableInvoices.Remove(this.GetSingle(id, companyId));
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






        public List<PurchasePrintout> PurchasePrintout(long companyId, long sobId, DateTime fromDate, DateTime toDate, string invoiceNo, long vendorId, long vendorSiteId)
        {
            var data = (from a in this.Context.PayableInvoices
                            join b in this.Context.PayableInvoiceDetails on a.Id equals b.InvoiceId
                            join c in this.Context.CodeCombinitions on b.CodeCombinationId equals c.Id
                            join d in this.Context.Vendors on a.VendorId equals d.Id
                            join e in this.Context.VendorSites on a.VendorSiteId equals e.Id
                            join f in this.Context.Withholdings on a.WHTaxId equals f.Id into g
                            from h in g.DefaultIfEmpty()
                            join i in this.Context.AccountValues on c.Segment1 equals i.Value into segment1_join
                            from j in segment1_join.DefaultIfEmpty()
                            join k in this.Context.AccountValues on c.Segment2 equals k.Value into segment2_join
                            from l in segment2_join.DefaultIfEmpty()
                            join m in this.Context.AccountValues on c.Segment3 equals m.Value into segment3_join
                            from n in segment3_join.DefaultIfEmpty()
                            join o in this.Context.AccountValues on c.Segment4 equals o.Value into segment4_join
                            from p in segment4_join.DefaultIfEmpty()
                            join q in this.Context.AccountValues on c.Segment5 equals q.Value into segment5_join
                            from r in segment5_join.DefaultIfEmpty()
                            join s in this.Context.AccountValues on c.Segment6 equals s.Value into segment6_join
                            from t in segment6_join.DefaultIfEmpty()
                            join u in this.Context.AccountValues on c.Segment7 equals u.Value into segment7_join
                            from v in segment7_join.DefaultIfEmpty()
                            join w in this.Context.AccountValues on c.Segment8 equals w.Value into segment8_join
                            from x in segment8_join.DefaultIfEmpty()

                            where a.CompanyId == companyId && a.SOBId == sobId &&
                            a.InvoiceDate >= fromDate && a.InvoiceDate <= toDate
                            select new PurchasePrintout
                            {
                                Amount = b.Amount,
                                CCSegment1 = j.ValueName,
                                CCSegment2 = l.ValueName,
                                CCSegment3 = n.ValueName,
                                CCSegment4 = p.ValueName,
                                CCSegment5 = r.ValueName,
                                CCSegment6 = t.ValueName,
                                CCSegment7 = v.ValueName,
                                CCSegment8 = x.ValueName,
                                Description = b.Description,
                                InvoiceDate = a.InvoiceDate,
                                InvoiceNo = a.InvoiceNo,
                                VendorId = a.VendorId,
                                VendorName = d.Name,
                                VendorSite = e.Name,
                                VendorSiteId = a.VendorSiteId,
                                WHTaxName = h.Description
                            }).ToList();

            if (invoiceNo != "" && invoiceNo != null)
                data = data.Where(x => x.InvoiceNo == invoiceNo).ToList();

            if (vendorId != 0)
                data = data.Where(x => x.VendorId == vendorId).ToList();

            if (vendorSiteId != 0)
                data = data.Where(x => x.VendorSiteId == vendorSiteId).ToList();

            return data;
        }
    }
}
