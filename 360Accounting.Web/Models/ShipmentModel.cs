using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class ShipmentListModel
    {
        [Display(Name = "Warehouse")]
        public long WarehouseId { get; set; }

        [Display(Name = "Customer")]
        public long CustomerId { get; set; }

        [Display(Name = "Customer Site")]
        public long CustomerSiteId { get; set; }

        [Display(Name = "Order Number")]
        public long OrderId { get; set; }

        public List<SelectListItem> Warehouses { get; set; }
        public List<SelectListItem> Customers { get; set; }
        public List<SelectListItem> CustomerSites { get; set; }
        public List<SelectListItem> Orders { get; set; }

        public IList<ShipmentDetailModel> ShipmentDetails { get; set; }

    }

    public class ShipmentDetailModel
    {
        public long OrderId { get; set; }
        public long LineId { get; set; }
        public long WarehouseId { get; set; }
        public long LocatorId { get; set; }
        public string LotNo { get; set; }
        public string SerialNo { get; set; }
        public decimal OrderQuantity { get; set; }
        public decimal ShipedQuantity { get; set; }
        public decimal BalanceQuantity { get; set; }
        public decimal ThisShipQuantity { get; set; }
    }

    public class ShipmentModel : ModelBase
    {
        #region Constructors

        #endregion

        #region Properties
        public long Id { get; set; }
        public long CompanyId { get; set; }
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