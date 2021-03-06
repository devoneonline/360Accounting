﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class PaymentHeader : EntityBase
    {
        public long Id { get; set; }

        public string PaymentNo { get; set; }

        public DateTime PaymentDate { get; set; }

        public long PeriodId { get; set; }

        public long VendorId { get; set; }

        public long VendorSiteId { get; set; }

        public long BankId { get; set; }

        public long BankAccountId { get; set; }

        public long SOBId { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }

        public long CompanyId { get; set; }
    }


    public class PaymentInvoiceLines : EntityBase
    {
        public long Id { get; set; }

        public long PaymentId { get; set; }

        public long InvoiceId { get; set; }

        public decimal Amount { get; set; }
    }
}
