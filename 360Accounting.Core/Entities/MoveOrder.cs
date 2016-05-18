using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class MoveOrder : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public string MoveOrderNo { get; set; }
        public DateTime MoveOrderDate { get; set; }
        public string Description { get; set; }
        public DateTime DateRequired { get; set; }
    }

    public class MoveOrderDetail : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long MoveOrderId { get; set; }
        public long ItemId { get; set; }
        public long LocatorId { get; set; }
        public long WarehouseId { get; set; }
        public string LotNo { get; set; }
        public string SerialNo { get; set; }
        public DateTime? DateRequired { get; set; }
        public decimal Quantity { get; set; }
    }

    public class LotNumber : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long ItemId { get; set; }
        public string LotNo { get; set; }
        public string SourceType { get; set; }
        public long SourceId { get; set; }
    }

    public class SerialNumber : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long LotNoId { get; set; }
        public string SerialNo { get; set; }
        public long CompanyId { get; set; }

        //Temporary for MoveOrder, will be removed.
        public string LotNo { get; set; }
    }
}
