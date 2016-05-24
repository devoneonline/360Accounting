using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Requisition : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long BuyerId { get; set; }
        public string RequisitionNo { get; set; }
        public DateTime RequisitionDate { get; set; }
        public string Description { get; set; }
    }

    public class RequisitionDetail : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long RequisitionId { get; set; }
        public long ItemId { get; set; }
        public long VendorId { get; set; }
        public long VendorSiteId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime NeedByDate { get; set; }
        public string Status { get; set; }
    }
}
