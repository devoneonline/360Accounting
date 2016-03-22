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
        [Display(Name = "Start Date")]
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
}