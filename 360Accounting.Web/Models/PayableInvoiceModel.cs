using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class PayableInvoiceListModel
    {
        [Display(Name = "Set Of Book")]
        public List<SelectListItem> SetOfBooks { get; set; }

        [Display(Name = "Period")]
        public List<SelectListItem> Periods { get; set; }

        public long SOBId { get; set; }
        public long PeriodId { get; set; }
    }

    public class PayableInvoiceModel : ModelBase
    {
        #region Properties
        public long CompanyId { get; set; }
        public long Id { get; set; }
        public long SOBId { get; set; }

        [Required]
        public long VendorId { get; set; }

        [Required]
        public long VendorSiteId { get; set; }
        public long PeriodId { get; set; }

        [Required]
        public long InvoiceTypeId { get; set; }

        [Display(Name = "With Holding Tax")]
        public List<SelectListItem> WHTaxes { get; set; }

        [Display(Name = "Vendor")]
        public List<SelectListItem> Vendors { get; set; }

        [Display(Name = "Vendor Site")]
        public List<SelectListItem> VendorSites { get; set; }

        [Display(Name = "Invoice Type")]
        public List<SelectListItem> InvoiceTypes { get; set; }

        [Required]
        [Display(Name = "WH Tax")]
        public long? WHTaxId { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Description")]
        public string Remarks { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public IList<PayableInvoiceDetailModel> InvoiceDetail { get; set; }
        #endregion

        #region Constructors
        public PayableInvoiceModel()
        {
        }

        public PayableInvoiceModel(PayableInvoice entity)
        {
            this.Amount = entity.Amount;
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.Id = entity.Id;
            this.InvoiceDate = entity.InvoiceDate;
            this.InvoiceNo = entity.InvoiceNo;
            this.InvoiceTypeId = entity.InvoiceTypeId;
            this.PeriodId = entity.PeriodId;
            this.Remarks = entity.Remarks;
            this.SOBId = entity.SOBId;
            this.Status = entity.Status;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
            this.VendorId = entity.VendorId;
            this.VendorSiteId = entity.VendorSiteId;
            this.WHTaxId = entity.WHTaxId;
        }
        #endregion
    }

    public class PayableInvoiceDetailModel : ModelBase
    {
        #region Properties
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public long CodeCombinationId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        #endregion

        #region Constructors
        public PayableInvoiceDetailModel()
        {
        }

        public PayableInvoiceDetailModel(PayableInvoiceDetail entity)
        {
            this.Amount = entity.Amount;
            this.CodeCombinationId = entity.CodeCombinationId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.Description = entity.Description;
            this.Id = entity.Id;
            this.InvoiceId = entity.InvoiceId;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
        #endregion
    }
}