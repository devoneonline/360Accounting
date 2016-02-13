using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevEx_360Accounting_Web.Models;
using System.Collections.Generic;

namespace DevEx._360Accounting.Web.Reports
{
    public partial class UserwiseSession : DevExpress.XtraReports.UI.XtraReport
    {
        public UserwiseSession()
        {
            InitializeComponent();
        }

        ////??????
        public XtraReport Report { get; set; }
        ////??????

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string UserNameFilter { get; set; }

        public List<UserViewModel> Users { get; set; }
    }
}
