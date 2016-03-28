using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Item : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long COGSCodeCombinationId { get; set; }
        public long SalesCodeCombinationId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string DefaultBuyer { get; set; }
        public string ReceiptRouting { get; set; }
        public bool LotControl { get; set; }
        public bool SerialControl { get; set; }
        public bool Purchaseable { get; set; }
        public bool Orderable { get; set; }
        public bool Shipable { get; set; }
    }

    public class ItemWarehouse : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long SOBId { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public string WarehouseCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
