using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class PurchasePrintout
    {
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string VendorName { get; set; }
        public string VendorSite { get; set; }
        public string WHTaxName { get; set; }

        public string CCSegment1 { get; set; }
        public string CCSegment2 { get; set; }
        public string CCSegment3 { get; set; }
        public string CCSegment4 { get; set; }
        public string CCSegment5 { get; set; }
        public string CCSegment6 { get; set; }
        public string CCSegment7 { get; set; }
        public string CCSegment8 { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
