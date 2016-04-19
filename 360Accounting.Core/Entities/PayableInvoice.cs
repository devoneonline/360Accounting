using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class PayableInvoice : EntityBase
    {
        [Key]
        public long Id { get; set; }
        //public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long VendorId { get; set; }
        public long VendorSiteId { get; set; }
        public long PeriodId { get; set; }
        public long InvoiceTypeId { get; set; }
        public long? WHTaxId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
    }

    public class PayableInvoiceDetail : EntityBase
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public long CodeCombinationId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
