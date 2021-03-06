﻿using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class Segment
    {
        public int SegmentCount { get; set; }

        public string SegmentName { get; set; }

        public List<SelectListItem> SegmentValueList { get; set; }

        public string SegmentValue { get; set; }

        public string SegmentValueName { get; set; }
    }

    public class CodeCombinitionCreateViewModel : ModelBase
    {
        #region Constructors
        public CodeCombinitionCreateViewModel()
        {
        }

        public CodeCombinitionCreateViewModel(CodeCombinition entity)
        {
            if (entity != null)
            {
                this.AllowedPosting = entity.AllowedPosting;
                this.CompanyId = entity.CompanyId;
                this.StartDate = entity.StartDate;
                this.EndDate = entity.EndDate;
                this.Id = entity.Id;
                this.Segment1 = entity.Segment1;
                this.Segment2 = entity.Segment2;
                this.Segment3 = entity.Segment3;
                this.Segment4 = entity.Segment4;
                this.Segment5 = entity.Segment5;
                this.Segment6 = entity.Segment6;
                this.Segment7 = entity.Segment7;
                this.Segment8 = entity.Segment8;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
            }
        }
        #endregion

        #region Properties
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Allow Posting")]
        public bool AllowedPosting { get; set; }

        public List<Segment> SegmentList { get; set; }
        #endregion
    }
}