using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class CalendarViewModel
    {
        #region Constructors
        public CalendarViewModel()
        {
        }

        public CalendarViewModel(Calendar entity)
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
        }
        #endregion

        #region Properties
        public long Id { get; set; }

        public long SOBId { get; set; }

        [Required]
        [Display(Name="Period")]
        public string PeriodName { get; set; }

        [Required]
        [Display(Name = "Year")]
        public int? PeriodYear { get; set; }

        [Required]
        [Display(Name = "Quarter")]
        public int? PeriodQuarter { get; set; }

        [Required]
        [Display(Name = "Sequence Number")]
        public int? SeqNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "From")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "To")]
        public DateTime EndDate { get; set; }

        [Required]
        public bool Adjusting { get; set; }

        [Display(Name = "Status")]
        public string ClosingStatus { get; set; }

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

        public List<SelectListItem> SetOfBooks { get; set; }
        #endregion
    }
}