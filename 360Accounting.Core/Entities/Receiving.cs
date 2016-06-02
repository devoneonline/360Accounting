using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Receiving : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime Date { get; set; }
        public long POId { get; set; }
        public string DCNo { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public bool Confirmed { get; set; }
    }

    public class ReceivingDetail : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long ReceiptId { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public long WarehouseId { get; set; }
        public long LocatorId { get; set; }
        public long? LotNoId { get; set; }
        public string SerialNo { get; set; }
        public long PODetailId { get; set; }
    }
}
