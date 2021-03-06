﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class ReceiptListModel
    {
        private string sortColumn = string.Empty;
        private string sortDirection = "ASC";

        #region Properties
        public string SearchText { get; set; }

        public int? Page { get; set; }

        public int TotalRecords { get; set; }

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

        public long PeriodId { get; set; }

        [Display(Name = "Periods")]
        public List<SelectListItem> Periods { get; set; }

        public long CustomerId { get; set; }

        [Display(Name = "Customers")]
        public List<SelectListItem> Customers { get; set; }

        public long CurrencyId { get; set; }

        [Display(Name = "Currency")]
        public List<SelectListItem> Currency { get; set; }

        public IEnumerable<ReceiptViewModel> Receipts { get; set; }

        #endregion
    }
}