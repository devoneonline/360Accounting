﻿using _360Accounting.Common;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class CalendarViewModel : ModelBase
    {
        #region Constructors
        public CalendarViewModel()
        {
            this.StartDate = Const.StartDate;
            this.EndDate = Const.EndDate;
        }

        public CalendarViewModel(Calendar entity)
        {
            if (entity != null)
            {
                this.Adjusting = entity.Adjusting;
                this.ClosingStatus = entity.ClosingStatus;
                this.EndDate = entity.EndDate;
                this.Id = entity.Id;
                this.PeriodName = entity.PeriodName;
                this.PeriodQuarter = entity.PeriodQuarter;
                this.PeriodYear = entity.PeriodYear;
                this.SeqNumber = entity.SeqNumber;
                this.SOBId = entity.SOBId;
                this.StartDate = entity.StartDate;
                this.CompanyId = entity.CompanyId;
                this.CreateBy = entity.CreateBy;
                this.CreateDate = entity.CreateDate;
                this.UpdateBy = entity.UpdateBy;
                this.UpdateDate = entity.UpdateDate;
            }
        }
        #endregion

        #region Properties
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long SOBId { get; set; }

        [Required]
        [Display(Name = "Period *")]
        [StringLength(30, ErrorMessage = "Period Name should not exceed length of 30 characters")]
        public string PeriodName { get; set; }

        [Required]
        [Display(Name = "Year *")]
        public int? PeriodYear { get; set; }

        [Required]
        [Display(Name = "Quarter *")]
        public int? PeriodQuarter { get; set; }

        [Required]
        [Display(Name = "Sequence Number")]
        public int? SeqNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date *")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date *")]
        public DateTime EndDate { get; set; }

        [Required]
        public bool Adjusting { get; set; }

        [Display(Name = "Status *")]
        public string ClosingStatus { get; set; }

        public string StartDateFormatted { get { return this.StartDate.ToString(Const.DATE_FORMAT_2); } }

        public string EndDateFormatted { get { return this.EndDate.ToString(Const.DATE_FORMAT_2); } }

        public List<SelectListItem> ClosingStatusList
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "Open", Value = "Open" });
                list.Add(new SelectListItem { Text = "Close", Value = "Close" });

                return list;
            }
        }
        #endregion
    }
}