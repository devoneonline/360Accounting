using DevEx_360Accounting_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevEx_360Accounting_Web
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

        public static CalendarViewModel Calendar
        {
            get
            {
                return HttpContext.Current.Session["Calendar"] == null ? new CalendarViewModel()
                    : (CalendarViewModel)HttpContext.Current.Session["Calendar"];
            }
            set
            {
                HttpContext.Current.Session["Calendar"] = value;
            }
        }
    }
}