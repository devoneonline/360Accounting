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

        public string PeriodName { get; set; }

        public int? PeriodYear { get; set; }

        public int? PeriodQuarter { get; set; }

        public int? SeqNumber { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Adjusting { get; set; }

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
        #endregion
    }
}