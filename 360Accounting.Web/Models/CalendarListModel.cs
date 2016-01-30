using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class CalendarListModel
    {
        private string sortColumn = string.Empty;
        private string sortDirection = "ASC";

        public string SearchText { get; set; }

        public int? Page { get; set; }

        public string SortColumn
        {
            get { return this.sortColumn; }
            set { this.sortColumn = value; }
        }

        public string SortDirection
        {
            get { return this.sortDirection; }
            set { this.sortDirection = value; }
        }

        public long SOBId { get; set; }

        [Display(Name = "Set Of Books")]
        public List<SelectListItem> SetOfBooks { get; set; }

        public IEnumerable<CalendarViewModel> Calendars { get; set; }
    }
}