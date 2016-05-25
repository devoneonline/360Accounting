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
        public long SOBId { get; set; }
    }

    public class PayableInvoiceModel : ModelBase
    {
        #region Properties
        public long CompanyId { get; set; }
        public long Id { get; set; }
        public long SOBId { get; set; }

        [Required(ErrorMessage = "Vendor is Required")]
        [Display(Name = "Vendor *")]
        public long VendorId { get; set; }

        [Required(ErrorMessage = "Vendor Site is Required")]
        [Display(Name = "Vendor Site *")]
        public long VendorSiteId { get; set; }

        [Required(ErrorMessage = "Period is Required")]
        [Display(Name = "Period *")]
        public long PeriodId { get; set; }

        [Required(ErrorMessage = "Invoice Type is Required")]
        [Display(Name = "Invoice Type *")]
        public long InvoiceTypeId { get; set; }

        public List<SelectListItem> WHTaxes { get; set; }
        public List<SelectListItem> Periods { get; set; }
        public List<SelectListItem> Vendors { get; set; }
        public List<SelectListItem> VendorSites { get; set; }
        public List<SelectListItem> InvoiceTypes { get; set; }

        [Display(Name = "With Holding Tax")]
        public long? WHTaxId { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }

        [Required(ErrorMessage = "Invoice Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Invoice Date *")]
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
            this.CompanyId = AuthenticationHelper.CompanyId.Value; // entity.CompanyId;
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

    public class PurchasePrintoutModel
    {
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string VendorName { get; set; }
        public string VendorSite { get; set; }
        public string WHTaxName { get; set; }

        //public long VendorId { get; set; }
        //public long VendorSiteId { get; set; }

        public string AccountCode { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }
    }

    public class PurchasePrintoutCriteriaModel
    {
        #region Constructors

        public PurchasePrintoutCriteriaModel()
        {
            this.Vendors = new List<SelectListItem>();
            this.VendorSites = new List<SelectListItem>();
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

        [Display(Name = "Invoice #")]
        public string InvoiceNo { get; set; }

        [Display(Name = "Vendor")]
        public long VendorId { get; set; }

        [Display(Name = "Vendor Site")]
        public long VendorSiteId { get; set; }

        public List<SelectListItem> Vendors { get; set; }
        public List<SelectListItem> VendorSites { get; set; }

        #endregion
    }
}