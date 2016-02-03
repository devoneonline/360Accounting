using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Mvc
{
    public class SessionHelper
    {
        public static JournalVoucherViewModel JournalVoucher
        {
            get
            {
                return HttpContext.Current.Session["JournalVoucher"] == null ? new JournalVoucherViewModel()
                    : (JournalVoucherViewModel)HttpContext.Current.Session["JournalVoucher"];
            }
            set
            {
                HttpContext.Current.Session["JournalVoucher"] = value;
            }
        }
    }
}