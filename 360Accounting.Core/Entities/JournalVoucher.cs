using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class JournalVoucher : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long SOBId { get; set; }

        public long CompanyId { get; set; }

        public long PeriodId { get; set; }

        public string JournalName { get; set; }

        public string Description { get; set; }

        public DateTime GLDate { get; set; }

        public string DocumentNo { get; set; }

        public bool PostingFlag { get; set; }

        public long CurrencyId { get; set; }

        public decimal ConversionRate { get; set; }
    }
}
