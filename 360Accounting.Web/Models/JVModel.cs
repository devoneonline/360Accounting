using _360Accounting.Common;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class GLHeaderModel
    {
        public long Id { get; set; }
        
        public long SOBId { get; set; }
        public long CompanyId { get; set; }
        public long PeriodId { get; set; }
        public long CurrencyId { get; set; }
        
        [Display(Name = "Journal Name")]
        public string JournalName { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Effective Date")]
        public DateTime GLDate { get; set; }

        [Display(Name = "Document No")]
        public string DocumentNo { get; set; }

        [Display(Name = "Conversion Rate")]
        public decimal ConversionRate { get; set; }

        public IList<GLLinesModel> GlLines { get; set; }

        public GLHeaderModel()
        {
            this.GLDate = Const.CurrentDate;
        }

        public GLHeaderModel(GLHeader entity)
        {
            this.Id = entity.Id;
            this.SOBId = entity.SOBId;
            this.CompanyId = entity.CompanyId;
            this.PeriodId = entity.PeriodId;
            this.CurrencyId = entity.CurrencyId;
            this.JournalName = entity.JournalName;
            this.Description = entity.Description;
            this.GLDate = entity.GLDate;
            this.DocumentNo = entity.DocumentNo;
            this.ConversionRate = entity.ConversionRate;
        }
    }


    public class GLLinesModel
    {
        public long Id { get; set; }
        public long HeaderId { get; set; }
        public long CodeCombinationId { get; set; }
        public decimal EnteredDr { get; set; }
        public decimal EnteredCr { get; set; }
        public decimal AccountedDr { get; set; }
        public decimal AccountedCr { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public long TaxRateCode { get; set; }

        public GLLinesModel()
        {
            this.EnteredCr = 0;
            this.EnteredDr = 0;
        }

        public GLLinesModel(GLLines entity)
        {
            this.Id = entity.Id;
            this.HeaderId = entity.HeaderId;
            this.CodeCombinationId = entity.CodeCombinationId;
            this.EnteredCr = entity.EnteredCr;
            this.EnteredDr = entity.EnteredDr;
            this.AccountedCr = entity.AccountedCr;
            this.AccountedDr = entity.AccountedDr;
            this.Quantity = entity.Qty;
            this.Description = entity.Description;
            this.TaxRateCode = entity.TaxRateCode;
        }
    }
}