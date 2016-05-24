using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class PurchaseOrder : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long VendorId { get; set; }
        public long VendorSiteId { get; set; }
        public long BuyerId { get; set; }
        public string PONo { get; set; }
        public DateTime PODate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }

    public class PurchaseOrderDetail : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long POId { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime NeedByDate { get; set; }
    }
}
