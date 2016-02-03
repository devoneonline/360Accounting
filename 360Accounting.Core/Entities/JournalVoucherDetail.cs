using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class JournalVoucherDetail : EntityBase
    {
        [Key]
        public long Id { get; set; }

        ////[ForeignKey("HeaderId")]
        public long HeaderId { get; set; }

        public long CodeCombinationId { get; set; }

        public decimal EnteredDr { get; set; }

        public decimal EnteredCr { get; set; }

        public decimal AccountedDr { get; set; }

        public decimal AccountedCr { get; set; }

        public decimal Qty { get; set; }

        public string Description { get; set; }

        public long TaxRateCode { get; set; }
    }
}
