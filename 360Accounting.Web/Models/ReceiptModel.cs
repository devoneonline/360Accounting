using _360Accounting.Common;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class ReceiptModel : ModelBase
    {
        #region Constructors
        public ReceiptModel()
        {
        }

        public ReceiptModel(Receipt entity)
        {
            this.BankAccountId = entity.BankAccountId;
            this.BankId = entity.BankId;
            this.ConversionRate = entity.ConversionRate;
            this.Id = entity.Id;
            this.CurrencyId = entity.CurrencyId;
            this.CustomerId = entity.CustomerId;
            this.CustomerSiteId = entity.CustomerSiteId;
            this.PeriodId = entity.PeriodId;
            this.ReceiptDate = entity.ReceiptDate;
            this.ReceiptNumber = entity.ReceiptNumber;
            this.Remarks = entity.Remarks;
            this.SOBId = entity.SOBId;
            this.Status = entity.Status;
            this.ReceiptAmount = entity.ReceiptAmount;
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
        #endregion

        #region Properties
        public long Id { get; set; }

        public long CompanyId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receipt Date")]
        public DateTime ReceiptDate { get; set; }
        
        public string ReceiptNumber { get; set; }

        public decimal ReceiptAmount { get; set; }

        public long CurrencyId { get; set; }

        public decimal ConversionRate { get; set; }

        public string Remarks { get; set; }

        public string Status { get; set; }

        public long CustomerId { get; set; }

        public long CustomerSiteId { get; set; }

        public long BankId { get; set; }

        public long BankAccountId { get; set; }

        public long SOBId { get; set; }

        public long PeriodId { get; set; }

        #endregion
    }

    public class ReceiptAuditTrialCriteriaModel
    {
        #region Constructors

        public ReceiptAuditTrialCriteriaModel()
        {
            this.FromDate = Const.StartDate;
            this.ToDate = Const.EndDate;
        }

        #endregion

        #region Properties

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }

        #endregion
    }

    public class ReceiptAuditTrialModel
    {
        public string ReceiptNo { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSiteName { get; set; }
        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }

    public class ReceiptPrintoutModel
    {
        public decimal ReceiptAmount { get; set; }
        public string AmountInWords { get; set; }
        public string CustomerName { get; set; }
        public string Remarks { get; set; }
    }

    public class ReceiptPrintoutCriteriaModel
    {
        #region Constructors

        public ReceiptPrintoutCriteriaModel()
        {
            this.Customers = new List<SelectListItem>();
            this.CustomerSites = new List<SelectListItem>();
            this.FromDate = Const.StartDate;
            this.ToDate = Const.EndDate;
        }

        #endregion

        #region Properties

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }

        [Display(Name = "Receipt #")]
        public string ReceiptNo { get; set; }

        [Display(Name = "Customer")]
        public long CustomerId { get; set; }

        [Display(Name = "Customer Site")]
        public long CustomerSiteId { get; set; }

        public List<SelectListItem> Customers { get; set; }
        public List<SelectListItem> CustomerSites { get; set; }

        #endregion
    }
}