using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Calendar : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long SOBId { get; set; }

        public string PeriodName { get; set; }

        public bool Adjusting { get; set; }

        public int? PeriodYear { get; set; }

        public int? PeriodQuarter { get; set; }

        public int? SeqNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ClosingStatus { get; set; }
    }
}
