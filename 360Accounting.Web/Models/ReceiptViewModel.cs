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
    public class ReceiptViewModel : ModelBase
    {
        #region Constructors
        public ReceiptViewModel()
        {
            this.ReceiptDate = Const.CurrentDate;
        }

        public ReceiptViewModel(ReceiptView entity)
        {
            this.CustomerName = entity.CustomerName;
            this.BankAccountId = entity.BankAccountId;
            this.BankId = entity.BankId;
            this.ConversionRate = entity.ConversionRate;
            this.Id = entity.Id;
            this.CustomerId = entity.CustomerId;
            this.CustomerSiteId = entity.CustomerSiteId;
            this.PeriodId = entity.PeriodId;
            this.ReceiptDate = entity.ReceiptDate;
            this.ReceiptNumber = entity.ReceiptNumber;
            this.Remarks = entity.Remarks;
            this.SOBId = entity.SOBId;
            this.Status = entity.Status;
            this.ReceiptAmount = entity.ReceiptAmount;
            this.BankAccountName = entity.BankAccountName;
            this.BankName = entity.BankName;
            this.CustomerSiteName = entity.CustomerSiteName;
            this.CurrencyId = entity.CurrencyId;
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

        [Display(Name = "Receipt Number")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "Receipt Amount")]
        public decimal ReceiptAmount { get; set; }
        
        [Display(Name = "Conversion Rate")]
        public decimal ConversionRate { get; set; }

        public string Remarks { get; set; }

        public string Status { get; set; }

        public long CustomerId { get; set; }

        [Display(Name = "Customer Site")]
        public long CustomerSiteId { get; set; }

        public string CustomerSiteName { get; set; }

        public string CustomerName { get; set; }

        public List<SelectListItem> CustomerSites { get; set; }

        [Display(Name = "Bank")]
        public long BankId { get; set; }

        public string BankName { get; set; }

        public List<SelectListItem> Banks { get; set; }

        [Display(Name = "Bank Account")]
        public long BankAccountId { get; set; }

        public string BankAccountName { get; set; }

        public List<SelectListItem> BankAccounts { get; set; }

        public long SOBId { get; set; }

        public long PeriodId { get; set; }

        public long CurrencyId { get; set; }

        #endregion
    }
}