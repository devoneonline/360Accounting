using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Mvc
{
    public class SessionHelper
    {
        private const string SESSION_JV = "SESSION_JV";

        ////used in jv
        //public static long CurrencyId
        //{
        //    get
        //    {
        //        return Convert.ToInt64(HttpContext.Current.Session["CurrencyId"].ToString());
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session["CurrencyId"] = value;
        //    }
        //}

        ////used in jv
        //public static long PeriodId
        //{
        //    get
        //    {
        //        return Convert.ToInt64(HttpContext.Current.Session["PeriodId"].ToString());
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session["PeriodId"] = value;
        //    }
        //}

        //used in customer sites

        public static GLHeaderModel JV
        {
            get
            {
                return HttpContext.Current.Session[SESSION_JV] == null ? null :
                    (GLHeaderModel)HttpContext.Current.Session[SESSION_JV];
            }
            set
            {
                HttpContext.Current.Session[SESSION_JV] = value;
            }
        }

        public static long SOBId
        {
            get
            {
                return Convert.ToInt64(HttpContext.Current.Session["SOBId"].ToString());
            }
            set
            {
                HttpContext.Current.Session["SOBId"] = value;
            }
        }

        //public static JournalVoucherViewModel JournalVoucher
        //{
        //    get
        //    {
        //        return HttpContext.Current.Session["JournalVoucher"] == null ? new JournalVoucherViewModel()
        //            : (JournalVoucherViewModel)HttpContext.Current.Session["JournalVoucher"];
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session["JournalVoucher"] = value;
        //    }
        //}

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