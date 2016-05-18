using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Shipment : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long OrderId { get; set; }
        public long LineId { get; set; }
        public long WarehouseId { get; set; }
        public long LocatorId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal Quantity { get; set; }
        public long? LotNoId { get; set; }
        public string SerialNo { get; set; }
        public string DeliveryNo { get; set; }
    }
}
