using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class RequisitionListModel
    {
        public IList<OrderModel> Orders { get; set; }
    }

    public class RequisitionModel : ModelBase
    {
        #region Constructor
        public RequisitionModel()
	    {
            this.Customers = new List<SelectListItem>();
            this.CustomerSites = new List<SelectListItem>();
            this.OrderDetail = new List<OrderDetailModel>();
            this.OrderTypes = new List<SelectListItem>();
	    }

        public RequisitionModel(Order entity)
        {
            if (entity != null)
            {
                this.CompanyId = entity.CompanyId;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.CustomerId = entity.CustomerId;
                this.CustomerSiteId = entity.CustomerSiteId;
                this.Id = entity.Id;
                this.OrderDate = entity.OrderDate;
                this.OrderNo = entity.OrderNo;
                this.OrderTypeId = entity.OrderTypeId;
                this.Remarks = entity.Remarks;
                this.Status = entity.Status;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
            }
        }

        public RequisitionModel(OrderView entityView, bool isForIndex)
        {
            if (entityView != null)
            {
                this.CompanyId = entityView.CompanyId;
                this.CreateBy = entityView.CreateBy;
                this.CreateDate = entityView.CreateDate;
                this.CustomerId = entityView.CustomerId;
                this.CustomerSiteId = entityView.CustomerSiteId;
                this.Id = entityView.Id;
                this.OrderDate = entityView.OrderDate;
                this.OrderNo = entityView.OrderNo;
                this.OrderTypeId = entityView.OrderTypeId;
                this.Remarks = entityView.Remarks;
                this.Status = entityView.Status;
                this.UpdateBy = entityView.UpdateBy;
                this.UpdateDate = entityView.UpdateDate;
                this.CustomerName = entityView.CustomerName;
                this.CustomerSiteName = entityView.CustomerSiteName;
                this.OrderTypeName = entityView.OrderTypeName;
            }
        }
        #endregion

        #region Properties
        public long Id { get; set; }
        public long CompanyId { get; set; }

        [Required(ErrorMessage = "Order Type is required")]
        [Display(Name = "Order Type *")]
        public long OrderTypeId { get; set; }

        public List<SelectListItem> OrderTypes { get; set; }

        [Required(ErrorMessage = "Order No. is required")]
        [Display(Name = "Order # *")]
        public string OrderNo { get; set; }

        [Required(ErrorMessage = "Order Date is required")]
        [Display(Name = "Order Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        [Display(Name = "Customer *")]
        public long CustomerId { get; set; }

        public List<SelectListItem> Customers { get; set; }

        [Required(ErrorMessage = "Customer Site is required")]
        [Display(Name = "Customer Site *")]
        public long CustomerSiteId { get; set; }

        public List<SelectListItem> CustomerSites { get; set; }

        public string Remarks { get; set; }
        public string Status { get; set; }

        public IList<OrderDetailModel> OrderDetail { get; set; }

        public string CustomerName { get; set; }
        public string CustomerSiteName { get; set; }
        public string OrderTypeName { get; set; }
        #endregion
    }

    public class RequisitionDetailModel : ModelBase
    {
        public RequisitionDetailModel()
        { }

        public RequisitionDetailModel(OrderDetail entity)
        {
            if (entity != null)
            {
                this.Amount = entity.Amount;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.Id = entity.Id;
                this.ItemId = entity.ItemId;
                this.OrderId = entity.OrderId;
                this.Quantity = entity.Quantity;
                this.Rate = entity.Rate;
                this.TaxId = entity.TaxId;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
                this.WarehouseId = entity.WarehouseId;
            }
        }

        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long TaxId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}