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
        public IList<RequisitionModel> Orders { get; set; }
    }

    public class RequisitionModel : ModelBase
    {
        #region Constructor
        public RequisitionModel()
	    {
            this.Buyers = new List<SelectListItem>();
            this.RequisitionDetail = new List<RequisitionDetailModel>();
	    }

        public RequisitionModel(Requisition entity)
        {
            if (entity != null)
            {
                this.BuyerId = entity.BuyerId;
                this.CompanyId = entity.CompanyId;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.Description = entity.Description;
                this.Id = entity.Id;
                this.RequisitionDate = entity.RequisitionDate;
                this.RequisitionNo = entity.RequisitionNo;
                this.SOBId = entity.SOBId;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
            }
        }

        public RequisitionModel(RequisitionView entityView, bool isForIndex)
        {
            if (entityView != null)
            {
                this.BuyerId = entityView.BuyerId;
                this.BuyerName = entityView.BuyerName;
                this.CompanyId = entityView.CompanyId;
                this.CreateBy = entityView.CreateBy;
                this.CreateDate = entityView.CreateDate;
                this.Description = entityView.Description;
                this.Id = entityView.Id;
                this.RequisitionDate = entityView.RequisitionDate;
                this.RequisitionNo = entityView.RequisitionNo;
                this.SOBId = entityView.SOBId;
                this.UpdateBy = entityView.UpdateBy;
                this.UpdateDate = entityView.UpdateDate;
            }
        }
        #endregion

        #region Properties
        public long Id { get; set; }
        
        public long CompanyId { get; set; }
        
        public long SOBId { get; set; }

        [Required(ErrorMessage = "Buyer is required")]
        [Display(Name = "Buyer *")]
        public long BuyerId { get; set; }

        public List<SelectListItem> Buyers { get; set; }

        public string BuyerName { get; set; }

        [Display(Name="Requisition No.")]
        public string RequisitionNo { get; set; }

        [Required(ErrorMessage = "Requisition Date is required")]
        [Display(Name = "Requisition Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RequisitionDate { get; set; }

        public string Description { get; set; }

        public IList<RequisitionDetailModel> RequisitionDetail { get; set; }

        #endregion
    }

    public class RequisitionDetailModel : ModelBase
    {
        public RequisitionDetailModel()
        { }

        public RequisitionDetailModel(RequisitionDetail entity)
        {
            if (entity != null)
            {
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.Id = entity.Id;
                this.ItemId = entity.ItemId;
                this.NeedByDate = entity.NeedByDate;
                this.Price = entity.Price;
                this.Quantity = entity.Quantity;
                this.RequisitionId = entity.RequisitionId;
                this.Status = entity.Status;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
                this.VendorId = entity.VendorId;
                this.VendorSiteId = entity.VendorSiteId;
            }
        }

        public long Id { get; set; }
        public long RequisitionId { get; set; }
        public long ItemId { get; set; }
        public long VendorId { get; set; }
        public long VendorSiteId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime NeedByDate { get; set; }
        public string Status { get; set; }
    }
}