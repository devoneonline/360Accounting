using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class ReceivingListModel
    {
        public IList<ReceivingModel> Receivings { get; set; }
    }

    public class ReceivingModel : ModelBase
    {
        #region Constructor
        public ReceivingModel()
        {
            this.ReceivingDetail = new List<ReceivingDetailModel>();
            POs = new List<SelectListItem>();
        }

        public ReceivingModel(Receiving entity)
        {
            if (entity != null)
            {
                this.CompanyId = entity.CompanyId;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.Date = entity.Date;
                this.DCNo = entity.DCNo;
                this.Id = entity.Id;
                this.POId = entity.POId;
                this.ReceiptNo = entity.ReceiptNo;
                this.SOBId = entity.SOBId;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
                this.Confirmed = entity.Confirmed;
            }
        }

        public ReceivingModel(ReceivingView entityView, bool isForIndex)
        {
            if (entityView != null)
            {
                this.CompanyId = entityView.CompanyId;
                this.CreateBy = entityView.CreateBy;
                this.CreateDate = entityView.CreateDate;
                this.Date = entityView.Date;
                this.DCNo = entityView.DCNo;
                this.Id = entityView.Id;
                this.POId = entityView.POId;
                this.ReceiptNo = entityView.ReceiptNo;
                this.SOBId = entityView.SOBId;
                this.UpdateBy = entityView.UpdateBy;
                this.UpdateDate = entityView.UpdateDate;
                this.PONo = entityView.PONo;
                this.Confirmed = entityView.Confirmed;
            }
        }
        #endregion

        #region Properties
        public long Id { get; set; }

        [Display(Name = "Receipt No.")]
        public string ReceiptNo { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public long POId { get; set; }

        [Display(Name="PO No.")]
        public string PONo { get; set; }

        public List<SelectListItem> POs { get; set; }

        [Display(Name = "DC No.")]
        public string DCNo { get; set; }

        public IList<ReceivingDetailModel> ReceivingDetail { get; set; }

        public long CompanyId { get; set; }

        public long SOBId { get; set; }

        public bool Confirmed { get; set; }

        #endregion
    }

    public class ReceivingDetailModel : ModelBase
    {
        public ReceivingDetailModel()
        { }

        public ReceivingDetailModel(ReceivingDetail entity)
        {
            if (entity != null)
            {
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.Id = entity.Id;
                this.ItemId = entity.ItemId;
                this.LocatorId = entity.LocatorId;
                this.LotNoId = entity.LotNoId;
                this.ThisPurchaseQty = entity.Quantity;
                this.ReceiptId = entity.ReceiptId;
                this.SerialNo = entity.SerialNo;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
                this.WarehouseId = entity.WarehouseId;
                this.PODetailId = entity.PODetailId;
            }
        }

        public ReceivingDetailModel(ReceivingDetailView entityView, bool isForIndex)
        {
            if (entityView != null)
            {
                this.CreateBy = entityView.CreateBy;
                this.CreateDate = entityView.CreateDate;
                this.Id = entityView.Id;
                this.ItemId = entityView.ItemId;
                this.LocatorId = entityView.LocatorId;
                this.LotNoId = entityView.LotNoId;
                this.ThisPurchaseQty = entityView.Quantity;
                this.ReceiptId = entityView.ReceiptId;
                this.SerialNo = entityView.SerialNo;
                this.UpdateBy = entityView.UpdateBy;
                this.UpdateDate = entityView.UpdateDate;
                this.WarehouseId = entityView.WarehouseId;
                this.ItemName = entityView.ItemName;
                this.LotNo = entityView.LotNo;
                this.PODetailId = entityView.PODetailId;
            }
        }

        public long Id { get; set; }
        public long ReceiptId { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ThisPurchaseQty { get; set; }
        public long WarehouseId { get; set; }
        public long LocatorId { get; set; }
        public long? LotNoId { get; set; }
        public string LotNo { get; set; }
        public string SerialNo { get; set; }
        public decimal PurchaseQty { get; set; }
        public decimal BalanceQty { get; set; }
        public decimal OrderQty { get; set; }
        public long PODetailId { get; set; }
    }
}