using _360Accounting.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class PayablePeriod : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long CalendarId { get; set; }

        public string Status { get; set; }

        public long SOBId { get; set; }
    }
}
