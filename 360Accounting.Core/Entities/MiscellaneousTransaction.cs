using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class MiscellaneousTransaction : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }
        public long CodeCombinationId { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long LocatorId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string LotNo { get; set; }
        public string SerialNo { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
    }
}
