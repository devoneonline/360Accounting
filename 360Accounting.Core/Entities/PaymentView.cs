using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class PaymentHeaderView : EntityBase
    {
        public long Id { get; set; }

        public string PaymentNo { get; set; }

        public DateTime PaymentDate { get; set; }

        public long PeriodId { get; set; }

        public long VendorId { get; set; }

        public long VendorSite { get; set; }

        public string VendorSiteName { get; set; }

        public long BankId { get; set; }

        public long BankAccountId { get; set; }

        public string BankAccountName { get; set; }

        public long SOBId { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }
    }
}
