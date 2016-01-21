using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class AccountValue : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long ChartId { get; set; }

        public string Segment { get; set; }

        public string ValueName { get; set; }

        public string Value { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string AccountType { get; set; }

        public int Levl { get; set; }
    }
}
