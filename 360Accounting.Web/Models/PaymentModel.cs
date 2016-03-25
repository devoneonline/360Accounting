using _360Accounting.Core;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class PaymentModel : ModelBase
    {
        #region Constructor

        public PaymentModel(PaymentHeader entity)
        {
            this.Id = entity.Id;
            this.Amount = entity.Amount;
            this.BankAccountId = entity.BankAccountId;
            this.BankId = entity.BankId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.PaymentDate = entity.PaymentDate;
            this.PaymentNo = entity.PaymentNo;
            this.PeriodId = entity.PeriodId;
            this.SOBId = entity.SOBId;
            this.Status = entity.Status;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
            this.VendorId = entity.VendorId;
            this.VendorSiteId = entity.VendorSiteId;
        }

        public PaymentModel()
        {
            this.PaymentDate = DateTime.Now;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public string PaymentNo { get; set; }

        [Display(Name = "Payment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        public long PeriodId { get; set; }
        
        public long VendorId { get; set; }

        public long VendorSiteId { get; set; }
        
        public long BankId { get; set; }
        
        public long BankAccountId { get; set; }

        public long SOBId { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }

        #endregion
    }

    public class PaymentInvoiceLinesModel : ModelBase
    {
        public PaymentInvoiceLinesModel(PaymentInvoiceLines entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
                this.Amount = entity.Amount;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.InvoiceId = entity.InvoiceId;
                this.PaymentId = entity.PaymentId;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
            }
        }

        public PaymentInvoiceLinesModel(long paymentId)
        {
            this.PaymentId = paymentId;
        }
        public PaymentInvoiceLinesModel()
        {
        }

        #region Properties

        public long Id { get; set; }

        public long PaymentId { get; set; }

        public long InvoiceId { get; set; }

        public decimal Amount { get; set; }

        #endregion
    }

    public class PaymentListViewModel: PaymentModel
    {
        #region Constructor

        public PaymentListViewModel()
        {
            this.PaymentDate = DateTime.Now;
        }

        public PaymentListViewModel(PaymentHeaderView entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
                this.Amount = entity.Amount;
                this.BankAccountId = entity.BankAccountId;
                this.BankId = entity.BankId;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.PaymentDate = entity.PaymentDate;
                this.PaymentNo = entity.PaymentNo;
                this.PeriodId = entity.PeriodId;
                this.SOBId = entity.SOBId;
                this.Status = entity.Status;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
                this.VendorId = entity.VendorId;
                this.VendorSiteId = entity.VendorSiteId;
            }
        }

        #endregion

        #region Properties

        public List<SelectListItem> Vendor { get; set; }

        public List<SelectListItem> Bank { get; set; }

        public List<SelectListItem> SetOfBook { get; set; }

        public List<SelectListItem> Period { get; set; }

        public IEnumerable<PaymentViewModel> Payments { get; set; }

        #endregion
    }

    public class PaymentViewModel : PaymentModel
    {
        #region Constructor

        public PaymentViewModel()
        {
            this.PaymentDate = DateTime.Now;
        }

        public PaymentViewModel(PaymentHeaderView entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
                this.Amount = entity.Amount;
                this.BankAccountId = entity.BankAccountId;
                this.BankId = entity.BankId;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.PaymentDate = entity.PaymentDate;
                this.PaymentNo = entity.PaymentNo;
                this.PeriodId = entity.PeriodId;
                this.SOBId = entity.SOBId;
                this.Status = entity.Status;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
                this.VendorId = entity.VendorId;
                this.VendorSiteId = entity.VendorSiteId;
                this.VendorSiteName = entity.VendorSiteName;
                this.BankAccountName = entity.BankAccountName;
            }
        }

        #endregion

        #region Properties

        public string VendorSiteName { get; set; }

        public string BankAccountName { get; set; }

        public List<SelectListItem> VendorSite { get; set; }

        public List<SelectListItem> BankAccount { get; set; }

        public List<PaymentInvoiceLinesModel> PaymentInvoiceLines { get; set; }

        #endregion
    }
}