using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Order : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long OrderTypeId { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public long CustomerId { get; set; }
        public long CustomerSiteId { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    }

    public class OrderDetail : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long TaxId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
