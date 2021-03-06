﻿using _360Accounting.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class ReceiptView : EntityBase
    {
        /// <summary>
        /// added by uzair to use in remittance
        /// </summary>
        public string CustomerName { get; set; }

        public long CompanyId { get; set; }

        [Key]
        public long Id { get; set; }

        public DateTime ReceiptDate { get; set; }

        public string ReceiptNumber { get; set; }

        public decimal ReceiptAmount { get; set; }

        public long CurrencyId { get; set; }

        public decimal ConversionRate { get; set; }

        public string Remarks { get; set; }

        public string Status { get; set; }

        public long CustomerId { get; set; }

        public long CustomerSiteId { get; set; }

        public string CustomerSiteName { get; set; }

        public long BankId { get; set; }

        public string BankName { get; set; }

        public long BankAccountId { get; set; }

        public string BankAccountName { get; set; }

        public long SOBId { get; set; }

        public long PeriodId { get; set; }
    }
}
