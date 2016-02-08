using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Entities
{
    public class CodeCombinition : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long SOBId { get; set; }

        public long CompanyId { get; set; }

        public string Segment1 { get; set; }

        public string Segment2 { get; set; }

        public string Segment3 { get; set; }

        public string Segment4 { get; set; }

        public string Segment5 { get; set; }

        public string Segment6 { get; set; }

        public string Segment7 { get; set; }

        public string Segment8 { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool AllowedPosting { get; set; }
    }

    public class CodeCombinationView
    {
        public long Id { get; set; }

        public long SOBId { get; set; }

        public long CompanyId { get; set; }

        public string Segment1 { get; set; }

        public string Segment1Name { get; set; }

        public string Segment2 { get; set; }

        public string Segment2Name { get; set; }

        public string Segment3 { get; set; }

        public string Segment3Name { get; set; }

        public string Segment4 { get; set; }

        public string Segment4Name { get; set; }

        public string Segment5 { get; set; }

        public string Segment5Name { get; set; }

        public string Segment6 { get; set; }

        public string Segment6Name { get; set; }

        public string Segment7 { get; set; }

        public string Segment7Name { get; set; }

        public string Segment8 { get; set; }

        public string Segment8Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool AllowedPosting { get; set; }

    }
}
