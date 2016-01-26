using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class AccountViewModel
    {
        public AccountViewModel()
        {
        }

        public AccountViewModel(AccountView entity)
        {
            this.Id = entity.Id;
            this.SOBId = entity.SOBId;
            this.SOBName = entity.SOBName;
            this.Segments = entity.Segments;
            this.SegmentsLength = entity.SegmentsLength;
        }

        public long Id { get; set; }

        public long SOBId { get; set; }

        public string SOBName { get; set; }

        public string Segments { get; set; }

        public string SegmentsLength { get; set; }
    }

    public class AccountCreateViewModel
    {
        #region Constructors
        public AccountCreateViewModel()
        {
        }

        public AccountCreateViewModel(Account bo)
        {
            this.Id = bo.Id;
            this.CompanyId = bo.CompanyId;
            this.SOBId = bo.SOBId;
            this.SegmentChar1 = bo.SegmentChar1;
            this.SegmentChar2 = bo.SegmentChar2;
            this.SegmentChar3 = bo.SegmentChar3;
            this.SegmentChar4 = bo.SegmentChar4;
            this.SegmentChar5 = bo.SegmentChar5;
            this.SegmentChar6 = bo.SegmentChar6;
            this.SegmentChar7 = bo.SegmentChar7;
            this.SegmentChar8 = bo.SegmentChar8;
            this.SegmentEnabled1 = bo.SegmentEnabled1 ?? false;
            this.SegmentEnabled2 = bo.SegmentEnabled2 ?? false;
            this.SegmentEnabled3 = bo.SegmentEnabled3 ?? false;
            this.SegmentEnabled4 = bo.SegmentEnabled4 ?? false;
            this.SegmentEnabled5 = bo.SegmentEnabled5 ?? false;
            this.SegmentEnabled6 = bo.SegmentEnabled6 ?? false;
            this.SegmentEnabled7 = bo.SegmentEnabled7 ?? false;
            this.SegmentEnabled8 = bo.SegmentEnabled8 ?? false;
            this.SegmentName1 = bo.SegmentName1;
            this.SegmentName2 = bo.SegmentName2;
            this.SegmentName3 = bo.SegmentName3;
            this.SegmentName4 = bo.SegmentName4;
            this.SegmentName5 = bo.SegmentName5;
            this.SegmentName6 = bo.SegmentName6;
            this.SegmentName7 = bo.SegmentName7;
            this.SegmentName8 = bo.SegmentName8;
        }
        #endregion

        #region Properties
        public long Id { get; set; }
        
        [Required]
        public long SOBId { get; set; }

        [Display(Name = "Set of Book")]
        public List<SelectListItem> SetOfBooks { get; set; }

        public long CompanyId { get; set; }

        [Display(Name = "1")]
        [StringLength(30, ErrorMessage = "Segment1 should not exceed length of 30 characters!")]
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
        [Required]
        [RegularExpression("^[0-9]{1,1}$", ErrorMessage = "Segment size should be less than 10")]
        public int? SegmentChar1 { get; set; }

        [Display(Name = "Size")]
        [RegularExpression("^[0-9]{1,1}$")]
        public int? SegmentChar2 { get; set; }

        [Display(Name = "Enabled")]
        [RegularExpression("^[0-9]{1,1}$")]
        public int? SegmentChar3 { get; set; }

        [RegularExpression("^[0-9]{1,1}$")]
        public int? SegmentChar4 { get; set; }

        [RegularExpression("^[0-9]{1,1}$")]
        public int? SegmentChar5 { get; set; }

        [RegularExpression("^[0-9]{1,1}$")]
        public int? SegmentChar6 { get; set; }

        [RegularExpression("^[0-9]{1,1}$")]
        public int? SegmentChar7 { get; set; }

        [RegularExpression("^[0-9]{1,1}$")]
        public int? SegmentChar8 { get; set; }

        public bool SegmentEnabled1 { get; set; }

        public bool SegmentEnabled2 { get; set; }

        public bool SegmentEnabled3 { get; set; }

        public bool SegmentEnabled4 { get; set; }

        public bool SegmentEnabled5 { get; set; }

        public bool SegmentEnabled6 { get; set; }

        public bool SegmentEnabled7 { get; set; }

        public bool SegmentEnabled8 { get; set; }
        #endregion
    }
}