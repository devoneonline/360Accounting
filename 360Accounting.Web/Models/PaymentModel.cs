using _360Accounting.Common;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class PaymentHeaderModel : ModelBase
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public string PaymentNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        public long PeriodId { get; set; }

        public long VendorId { get; set; }

        public long VendorSite { get; set; }

        public long BankId { get; set; }

        public long BankAccountId { get; set; }

        public long SOBId { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }

        public IList<PaymentInvoiceLinesModel> PaymentInvoiceLines { get; set; }

        public PaymentHeaderModel()
        {
            this.PaymentDate = Const.CurrentDate;
        }

        public PaymentHeaderModel(PaymentHeader entity)
        {
            this.Id = entity.Id;
            this.SOBId = entity.SOBId;
            this.Amount = entity.Amount;
            this.BankAccountId = entity.BankAccountId;
            this.BankId = entity.BankId;
            this.PaymentDate = entity.PaymentDate;
            this.PaymentNo = entity.PaymentNo;
            this.PeriodId = entity.PeriodId;
            this.SOBId = entity.SOBId;
            this.Status = entity.Status;
            this.VendorId = entity.VendorId;
            this.VendorSite = entity.VendorSite;
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
    }


    public class PaymentInvoiceLinesModel : ModelBase
    {
        public long Id { get; set; }

        public long PaymentId { get; set; }

        public long InvoiceId { get; set; }

        public decimal Amount { get; set; }

        public PaymentInvoiceLinesModel()
        {
            this.Amount = 0;
        }

        public PaymentInvoiceLinesModel(PaymentInvoiceLines entity)
        {
            this.Id = entity.Id;
            this.Amount = entity.Amount;
            this.InvoiceId = entity.InvoiceId;
            this.PaymentId = entity.PaymentId;
        }
    }
}