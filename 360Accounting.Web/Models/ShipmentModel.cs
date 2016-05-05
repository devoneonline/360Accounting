using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class OrderShipmentModel:ModelBase
    {
        public OrderShipmentModel()
        {
            this.CreateDate = DateTime.Now;
            this.Customers = new List<SelectListItem>();
            this.CustomerSites = new List<SelectListItem>();
            this.Warehouses = new List<SelectListItem>();
            this.Orders = new List<SelectListItem>();
        }
        public OrderShipmentModel(ShipmentModel model, long? customerId, long? customerSiteId)
        {
            if (model != null)
            {
                this.Id = model.Id;
                this.DeliveryDate = model.DeliveryDate;
                this.OrderId = model.OrderId;
                this.WarehouseId = model.WarehouseId;
                this.CustomerId = customerId == null ? 0 : customerId.Value;
                this.CustomerSiteId = customerSiteId == null ? 0 : customerSiteId.Value;
            }
        }
        public long Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        
        [Display(Name = "Order Number")]
        public long OrderId { get; set; }

        [Display(Name = "Warehouse")]
        public long WarehouseId { get; set; }

        [Display(Name = "Customer")]
        public long CustomerId { get; set; }

        [Display(Name = "Customer Site")]
        public long CustomerSiteId { get; set; }

        public long CompanyId { get; set; }

        public List<SelectListItem> Orders { get; set; }
        public List<SelectListItem> Warehouses { get; set; }
        public List<SelectListItem> Customers { get; set; }
        public List<SelectListItem> CustomerSites { get; set; }

        public IList<OrderShipmentLine> OrderShipments { get; set; }
    }

    public class OrderShipmentLine
    {
        public OrderShipmentLine()
        { }
        public OrderShipmentLine(ShipmentModel model, string itemName)
        {
            if (model != null)
            {
                this.Id = model.Id;
                this.LineId = model.LineId;
                this.LocatorId = model.LocatorId;
                this.LotNo = model.LotNo;
                this.SerialNo = model.SerialNo;
                this.ItemName = itemName;
                this.ShipedQuantity = model.Quantity;
            }
        }

        public long Id { get; set; }
        public long LineId { get; set; }
        public long LocatorId { get; set; }
        //public long LotNoId { get; set; }
        public string LotNo { get; set; }
        //public long SerialNoId { get; set; }
        public string SerialNo { get; set; }
        //public long ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal OrderQuantity { get; set; }
        public decimal ShipedQuantity { get; set; }
        public decimal BalanceQuantity { get; set; }
        public decimal ThisShipQuantity { get; set; }
    }

    public class ShipmentModel : ModelBase
    {
        #region Constructors

        public ShipmentModel()
        { }

        public ShipmentModel(Shipment entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.DeliveryDate = entity.DeliveryDate;
                this.Id = entity.Id;
                this.LineId = entity.LineId;
                this.LocatorId = entity.LocatorId;
                this.LotNo = entity.LotNo;
                this.OrderId = entity.OrderId;
                this.Quantity = entity.Quantity;
                this.SerialNo = entity.SerialNo;
                this.SOBId = entity.SOBId;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
                this.WarehouseId = entity.WarehouseId;
            }
        }

        #endregion

        #region Properties
        public long Id { get; set; }
        public long SOBId { get; set; }
        public long OrderId { get; set; }
        public long LineId { get; set; }
        public long WarehouseId { get; set; }
        public long LocatorId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal Quantity { get; set; }
        public string LotNo { get; set; }
        public string SerialNo { get; set; }
        #endregion
    }
}