using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class Vendor : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long SOBId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ContactNo { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
