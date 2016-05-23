using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class RFQ : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long SOBId { get; set; }

        public long BuyerId { get; set; }

        public string RFQNo { get; set; }

        public DateTime RFQDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public string Status { get; set; }
    }

    public class RFQDetail : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long RFQId { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal TargetPrice { get; set; }
    }

    public class RFQView : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long BuyerId { get; set; }
        public string RFQNo { get; set; }
        public DateTime RFQDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Status { get; set; }
        public string BuyerName { get; set; }
    }
}
