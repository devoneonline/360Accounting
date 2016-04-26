using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Invoice : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long SOBId { get; set; }
        public long PeriodId { get; set; }
        public long CurrencyId { get; set; }
        public long CustomerId { get; set; }
        public long CustomerSiteId { get; set; }
        public long CompanyId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? ConversionRate { get; set; }
        public string Remarks { get; set; }
        ///TODO: Order Id to be added later.
        ////public long OrderId { get; set; }
    }

    public class InvoiceView : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long SOBId { get; set; }
        public long PeriodId { get; set; }
        public long CurrencyId { get; set; }
        public long CustomerId { get; set; }
        public long CustomerSiteId { get; set; }
        public long CompanyId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? ConversionRate { get; set; }
        public string Remarks { get; set; }
        public string PeriodName { get; set; }
        public string CurrencyName { get; set; }
    }

    public class InvoiceDetail : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public long? InvoiceSourceId { get; set; }
        public long? ItemId { get; set; }
        public long TaxId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
    }
}
