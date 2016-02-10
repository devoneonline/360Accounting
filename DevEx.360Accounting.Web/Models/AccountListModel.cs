using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevEx_360Accounting_Web.Models
{
    public class AccountListModel
    {
        private string sortColumn = "Segments";
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

        public IEnumerable<AccountViewModel> Accounts { get; set; }

        #endregion
    }
}