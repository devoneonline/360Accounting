using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class RFQListModel
    {
        public IList<RFQModel> RFQs { get; set; }
    }

    public class RFQModel : ModelBase
    {
        #region Properties
        
        public long Id { get; set; }

        public long CompanyId { get; set; }

        [Required(ErrorMessage = "Buyer is required")]
        [Display(Name = "Buyer *")]
        public long BuyerId { get; set; }

        [Display(Name = "RFQ # *")]
        public string RFQNo { get; set; }

        [Required(ErrorMessage = "RFQ Date is required")]
        [Display(Name = "RFQ Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RFQDate { get; set; }

        [Display(Name = "Close Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CloseDate { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        public List<SelectListItem> Buyers { get; set; }
        public string BuyerName { get; set; }

        public IList<RFQDetailModel> RFQDetail { get; set; }

        #endregion

        #region Constructors
        public RFQModel()
        {
            this.Buyers = new List<SelectListItem>();
        }

        public RFQModel(RFQ entity)
        {
            if (entity != null)
            {
                this.BuyerId = entity.BuyerId;
                this.CloseDate = entity.CloseDate;
                this.CompanyId = entity.CompanyId;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.Id = entity.Id;
                this.RFQDate = entity.RFQDate;
                this.RFQNo = entity.RFQNo;
                this.Status = entity.Status;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
            }
        }

        public RFQModel(RFQView entityView, bool isForIndex)
        {
            if (entityView != null)
            {
                this.BuyerId = entityView.BuyerId;
                this.BuyerName = entityView.BuyerName;
                this.CloseDate = entityView.CloseDate;
                this.CompanyId = entityView.CompanyId;
                this.CreateBy = entityView.CreateBy;
                this.CreateDate = entityView.CreateDate;
                this.Id = entityView.Id;
                this.RFQDate = entityView.RFQDate;
                this.RFQNo = entityView.RFQNo;
                this.Status = entityView.Status;
                this.UpdateBy = entityView.UpdateBy;
                this.UpdateDate = entityView.UpdateDate;
            }
        }
        #endregion
    }

    public class RFQDetailModel : ModelBase
    {
        #region Constructors

        public RFQDetailModel()
        {
        }

        public RFQDetailModel(RFQDetail entity)
        {
            if (entity != null)
            {
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.Id = entity.Id;
                this.ItemId = entity.ItemId;
                this.Quantity = entity.Quantity;
                this.RFQId = entity.RFQId;
                this.TargetPrice = entity.TargetPrice;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
            }
        }

        #endregion

        #region Properties

        public long Id { get; set; }
        public long RFQId { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal TargetPrice { get; set; }

        #endregion
    }
}