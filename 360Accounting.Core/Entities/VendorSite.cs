using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class VendorSite : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long VendorId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        //public long? TaxCodeId { get; set; }

        public long CodeCombinationId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class VendorSiteView 
    {
        public long Id { get; set; }

        public long VendorId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        //public long? TaxCodeId { get; set; }

        public long CodeCombinationId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        //public string TaxCodeName { get; set; }

        public CodeCombinition CodeCombination { get; set; }
    }

}
