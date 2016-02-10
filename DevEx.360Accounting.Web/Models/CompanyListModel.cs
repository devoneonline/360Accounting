using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevEx_360Accounting_Web.Models
{
    public class CompanyListModel
    {
        public IEnumerable<CompanyModel> Companies { get; set; }
    }
}