using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class JournalVoucherCreateModel
    {
        public long Id { get; set; }

        public long SOBId { get; set; }

        public long PeriodId { get; set; }

        public long CurrencyId { get; set; }

        public long CompanyId { get; set; }

        public string SOBName { get; set; }

        public string PeriodName { get; set; }

        public string CurrencyName { get; set; }

        public string JournalName { get; set; }

        public string Description { get; set; }

        public DateTime GLDate { get; set; }

        public string DocumentNo { get; set; }

        public bool PostingFlag { get; set; }

        public decimal ConversionRate { get; set; }

        public long CodeCombinationId { get; set; }

        public List<SelectListItem> CodeCombinationList { get; set; }

        public decimal EnteredDr { get; set; }

        public decimal EnteredCr { get; set; }

        public decimal AccountedDr { get; set; }

        public decimal AccountedCr { get; set; }

        public decimal Qty { get; set; }

        public string GLLinesDescription { get; set; }

        public long TaxRateCode { get; set; }
    }
}