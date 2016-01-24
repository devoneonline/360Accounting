﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class AccountValueListModel
    {
        private string sortColumn = "Segments";
        private string sortDirection = "ASC";

        #region Properties

        public string SearchText { get; set; }

        public int? Page { get; set; }

        public int TotalRecords { get; set; }

        public string SortColumn
        {
            get { return sortColumn; }
            set { sortColumn = value; }
        }

        public string SortDirection
        {
            get { return sortDirection; }
            set { sortDirection = value; }
        }

        [Display(Name = "Set Of Books")]
        public List<SelectListItem> SetOfBooks { get; set; }

        public long SOBId { get; set; }

        public List<SelectListItem> Segments { get; set; }

        public string Segment { get; set; }

        public List<AccountValueViewModel> AccountValues { get; set; }

        #endregion
    }
}