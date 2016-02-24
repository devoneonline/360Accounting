using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class JournalVoucherCreateModel
    {
        public long Id { get; set; }

        public long HeaderId { get; set; }

        public long SOBId { get; set; }

        public long PeriodId { get; set; }

        public long CurrencyId { get; set; }

        [Display(Name = "Set of Book")]
        public string SOBName { get; set; }

        [Display(Name = "Period")]
        public string PeriodName { get; set; }

        [Display(Name = "Currency")]
        public string CurrencyName { get; set; }

        [Display(Name = "Journal Name")]
        public string JournalName { get; set; }

        [Display(Name = "Journal Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Effective Date")]
        public DateTime GLDate { get; set; }

        [Display(Name = "Document #")]
        public string DocumentNo { get; set; }

        public bool PostingFlag { get; set; }

        [Display(Name = "Conversion Rate")]
        public decimal ConversionRate { get; set; }

        [Display(Name = "Account Code")]
        [Required]
        public long CodeCombinationId { get; set; }

        public List<SelectListItem> CodeCombinationList { get; set; }

        [Display(Name = "Debit")]
        public decimal EnteredDr { get; set; }

        [Display(Name = "Credit")]
        public decimal EnteredCr { get; set; }

        [Display(Name = "Acct-Dr")]
        public decimal AccountedDr { get; set; }

        [Display(Name = "Acct-Cr")]
        public decimal AccountedCr { get; set; }

        [Display(Name = "Qty")]
        public decimal Qty { get; set; }

        [Display(Name = "Description")]
        public string GLLinesDescription { get; set; }

        [Display(Name = "Tax Rate")]
        public long TaxRateCode { get; set; }
    }
}