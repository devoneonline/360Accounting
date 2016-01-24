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
    public class Account : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public long SOBId { get; set; }

        public long CompanyId { get; set; }

        public string SegmentName1 { get; set; }

        public string SegmentName2 { get; set; }

        public string SegmentName3 { get; set; }

        public string SegmentName4 { get; set; }

        public string SegmentName5 { get; set; }

        public string SegmentName6 { get; set; }

        public string SegmentName7 { get; set; }

        public string SegmentName8 { get; set; }

        public int? SegmentChar1 { get; set; }

        public int? SegmentChar2 { get; set; }

        public int? SegmentChar3 { get; set; }

        public int? SegmentChar4 { get; set; }

        public int? SegmentChar5 { get; set; }

        public int? SegmentChar6 { get; set; }

        public int? SegmentChar7 { get; set; }

        public int? SegmentChar8 { get; set; }

        public bool? SegmentEnabled1 { get; set; }

        public bool? SegmentEnabled2 { get; set; }

        public bool? SegmentEnabled3 { get; set; }

        public bool? SegmentEnabled4 { get; set; }

        public bool? SegmentEnabled5 { get; set; }

        public bool? SegmentEnabled6 { get; set; }

        public bool? SegmentEnabled7 { get; set; }

        public bool? SegmentEnabled8 { get; set; }
    }
}
