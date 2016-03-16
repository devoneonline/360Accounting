using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public class SessionHelper
    {
        private const string SESSION_JV = "SESSION_JV";
        private const string SESSION_TAX = "SESSION_Tax";
        private const string SESSION_INVOICE = "SESSION_Invoice";

        public static InvoiceModel Invoice
        {
            get
            {
                return HttpContext.Current.Session[SESSION_INVOICE] == null ? null :
                    (InvoiceModel)HttpContext.Current.Session[SESSION_INVOICE];
            }
            set
            {
                HttpContext.Current.Session[SESSION_INVOICE] = value;
            }
        }

        public static TaxModel Tax
        {
            get
            {
                return HttpContext.Current.Session[SESSION_TAX] == null ? null :
                    (TaxModel)HttpContext.Current.Session[SESSION_TAX];
            }
            set
            {
                HttpContext.Current.Session[SESSION_TAX] = value;
            }
        }

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

        //TODO: this is temporary because of the issue 40 in task list.
        public static int PrecisionLimit
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["PrecissionLimit"].ToString());
            }
            set
            {
                HttpContext.Current.Session["PrecissionLimit"] = value;
            }
        }

        //TODO: I am using it in JV to check the GL Date in GLHeader and Account in GL Lines..?
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