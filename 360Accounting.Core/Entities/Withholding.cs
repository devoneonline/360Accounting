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
    public class Withholding : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public long CodeCombinitionId { get; set; }

        public long VendorId { get; set; }

        public long VendorSiteId { get; set; }

        public decimal Rate { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public long SOBId { get; set; }
    }
}
