using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class MoveOrderListModel
    {
        public long SOBId { get; set; }

        public List<MoveOrderModel> Items { get; set; }
    }

    public class MoveOrderModel : ModelBase
    {
        #region Constructors
        public MoveOrderModel()
        {
        }

        public MoveOrderModel(MoveOrder entity)
        {
            this.CompanyId = entity.CompanyId;
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.DateRequired = entity.DateRequired;
            this.Description = entity.Description;
            this.Id = entity.Id;
            this.MoveOrderDate = entity.MoveOrderDate;
            this.MoveOrderNo = entity.MoveOrderNo;
            this.SOBId = entity.SOBId;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
        }
        #endregion

        #region Properties
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }

        [Display(Name = "Order #")]
        public string MoveOrderNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order Date")]
        public DateTime MoveOrderDate { get; set; }

        ////What is the difference b/w these 2???
        [MaxLength(255, ErrorMessage = "Description should not exceed 255 characters.")]
        [StringLength(255, ErrorMessage = "Description should not exceed 255 characters.")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Required Date")]
        public DateTime DateRequired { get; set; }

        public IList<MoveOrderDetailModel> MoveOrderDetail { get; set; }
        #endregion
    }

    public class MoveOrderDetailModel : ModelBase
    {
        #region Constructors
        public MoveOrderDetailModel()
        {
        }

        public MoveOrderDetailModel(MoveOrderDetail entity)
        {
            this.CreateBy = entity.CreateBy;
            this.CreateDate = entity.CreateDate;
            this.DateRequired = entity.DateRequired;
            this.Id = entity.Id;
            this.ItemId = entity.ItemId;
            this.LocatorId = entity.LocatorId;
            this.LotNo = entity.LotNo;
            this.MoveOrderId = entity.MoveOrderId;
            this.Quantity = entity.Quantity;
            this.SerialNo = entity.SerialNo;
            this.UpdateBy = entity.UpdateBy;
            this.UpdateDate = entity.UpdateDate;
            this.WarehouseId = entity.WarehouseId;
        }
        #endregion

        #region Properties
        public long Id { get; set; }
        public long MoveOrderId { get; set; }
        public long ItemId { get; set; }
        public long LocatorId { get; set; }
        public long WarehouseId { get; set; }
        public string LotNo { get; set; }
        public string SerialNo { get; set; }
        public DateTime? DateRequired { get; set; }
        public decimal Quantity { get; set; }
        #endregion
    }
}