using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class PaymentHeaderListModel
    {
        private string sortColumn = "";
        private string sortDirection = "ASC";

        #region Properties
        [Display(Name = "Set Of Book")]
        public List<SelectListItem> SetOfBooks { get; set; }

        public long SOBId { get; set; }

        [Display(Name = "Period")]
        public List<SelectListItem> Periods { get; set; }

        public long PeriodId { get; set; }

        [Display(Name = "Vendor")]
        public List<SelectListItem> Vendor { get; set; }

        public long VendorId { get; set; }

        [Display(Name = "Bank")]
        public List<SelectListItem> Bank { get; set; }

        public long BankId { get; set; }

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

        public IEnumerable<PaymentHeaderViewModel> PaymentHeaders { get; set; }

        #endregion
    }
}