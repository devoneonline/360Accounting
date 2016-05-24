using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class PurchaseOrderListModel
    {
        public IList<PurchaseOrderModel> PurchaseOrders { get; set; }
    }

    public class PurchaseOrderModel : ModelBase
    {
        #region Constructor
        public PurchaseOrderModel()
	    {
            this.Buyers = new List<SelectListItem>();
            this.Vendors = new List<SelectListItem>();
            this.VendorSites = new List<SelectListItem>();
            this.PurchaseOrderDetail = new List<PurchaseOrderDetailModel>();
	    }

        public PurchaseOrderModel(PurchaseOrder entity)
        {
            if (entity != null)
            {
                this.BuyerId = entity.BuyerId;
                this.CompanyId = entity.CompanyId;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.Description = entity.Description;
                this.Id = entity.Id;
                this.PODate = entity.PODate;
                this.PONo = entity.PONo;
                this.SOBId = entity.SOBId;
                this.Status = entity.Status;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
                this.VendorId = entity.VendorId;
                this.VendorSiteId = entity.VendorSiteId;
            }
        }

        public PurchaseOrderModel(PurchaseOrderView entityView, bool isForIndex)
        {
            if (entityView != null)
            {
                this.BuyerId = entityView.BuyerId;
                this.CompanyId = entityView.CompanyId;
                this.CreateBy = entityView.CreateBy;
                this.CreateDate = entityView.CreateDate;
                this.Description = entityView.Description;
                this.Id = entityView.Id;
                this.PODate = entityView.PODate;
                this.PONo = entityView.PONo;
                this.SOBId = entityView.SOBId;
                this.Status = entityView.Status;
                this.UpdateBy = entityView.UpdateBy;
                this.UpdateDate = entityView.UpdateDate;
                this.VendorId = entityView.VendorId;
                this.VendorSiteId = entityView.VendorSiteId;
                this.VendorName = entityView.VendorName;
                this.VendorSiteName = entityView.VendorSiteName;
                this.BuyerName = entityView.BuyerName;
            }
        }
        #endregion

        #region Properties
        public long Id { get; set; }
        
        public long CompanyId { get; set; }
        
        public long SOBId { get; set; }

        [Required(ErrorMessage = "Vendor is required")]
        [Display(Name = "Vendor *")]
        public long VendorId { get; set; }

        public List<SelectListItem> Vendors { get; set; }

        [Required(ErrorMessage = "Vendor Site is required")]
        [Display(Name = "Vendor Site *")]
        public long VendorSiteId { get; set; }

        public List<SelectListItem> VendorSites { get; set; }

        [Required(ErrorMessage = "Buyer is required")]
        [Display(Name = "Buyer *")]
        public long BuyerId { get; set; }

        public List<SelectListItem> Buyers { get; set; }

        public string VendorName { get; set; }
        public string VendorSiteName { get; set; }
        public string BuyerName { get; set; }

        [Display(Name="PO No.")]
        public string PONo { get; set; }

        [Required(ErrorMessage = "POn Date is required")]
        [Display(Name = "PO Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PODate { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public IList<PurchaseOrderDetailModel> PurchaseOrderDetail { get; set; }

        #endregion
    }

    public class PurchaseOrderDetailModel : ModelBase
    {
        public PurchaseOrderDetailModel()
        { }

        public PurchaseOrderDetailModel(PurchaseOrderDetail entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
                this.POId = entity.POId;
                this.ItemId = entity.ItemId;
                this.Quantity = entity.Quantity;
                this.Price = entity.Price;
                this.NeedByDate = entity.NeedByDate;
            }
        }

        public long Id { get; set; }
        public long POId { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime NeedByDate { get; set; }
    }
}