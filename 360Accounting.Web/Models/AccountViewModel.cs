using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class AccountViewModel
    {
        public long SOBId { get; set; }

        public long CompanyId { get; set; }

        [Display(Name = "1")]
        public string SegmentName1 { get; set; }

        [Display(Name = "2")]
        public string SegmentName2 { get; set; }

        [Display(Name = "3")]
        public string SegmentName3 { get; set; }

        [Display(Name = "4")]
        public string SegmentName4 { get; set; }

        [Display(Name = "5")]
        public string SegmentName5 { get; set; }

        [Display(Name = "6")]
        public string SegmentName6 { get; set; }

        [Display(Name = "7")]
        public string SegmentName7 { get; set; }

        [Display(Name = "8")]
        public string SegmentName8 { get; set; }

        [Display(Name = "Segments")]
        public int SegmentChar1 { get; set; }

        [Display(Name = "Size")]
        public int SegmentChar2 { get; set; }

        [Display(Name = "Enabled")]
        public int SegmentChar3 { get; set; }

        public int SegmentChar4 { get; set; }

        public int SegmentChar5 { get; set; }

        public int SegmentChar6 { get; set; }

        public int SegmentChar7 { get; set; }

        public int SegmentChar8 { get; set; }

        public bool SegmentEnabled1 { get; set; }

        public bool SegmentEnabled2 { get; set; }

        public bool SegmentEnabled3 { get; set; }

        public bool SegmentEnabled4 { get; set; }

        public bool SegmentEnabled5 { get; set; }

        public bool SegmentEnabled6 { get; set; }

        public bool SegmentEnabled7 { get; set; }

        public bool SegmentEnabled8 { get; set; }
    }
}