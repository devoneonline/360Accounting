using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class CustomerSite : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long CustomerId { get; set; }

        public string SiteName { get; set; }

        public string SiteAddress { get; set; }

        public string SiteContact { get; set; }

        public long? TaxCodeId { get; set; }

        public long CodeCombinationId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class CustomerSiteView : EntityBase
    {
        public long Id { get; set; }

        public long CustomerId { get; set; }

        public string SiteName { get; set; }

        public string SiteAddress { get; set; }

        public string SiteContact { get; set; }

        public long? TaxId { get; set; }

        public string TaxName { get; set; }

        public long CodeCombinationId { get; set; }

        public CodeCombinition CodeCombination { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
