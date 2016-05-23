using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class RequisitionView : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string RequisitionNo { get; set; }
        public DateTime RequisitionDate { get; set; }
        public string Description { get; set; }
    }
}
