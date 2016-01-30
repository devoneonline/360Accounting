using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
            this.SOBId = entity.SOBId;
            this.Adjusting = entity.Adjusting;
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

        public string PeriodName { get; set; }

        [Required]
        public int? PeriodYear { get; set; }

        [Required]
        public int? PeriodQuarter { get; set; }

        public int? SeqNumber { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Adjusting { get; set; }
        #endregion
    }
}