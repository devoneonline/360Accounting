using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class CodeCombinitionView
    {
        public long Id { get; set; }

        public long SOBId { get; set; }

        public long CompanyId { get; set; }

        public string CodeCombinitionCode { get; set; }

        public string CodeCombinitionName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool AllowedPosting { get; set; }
    }
}
