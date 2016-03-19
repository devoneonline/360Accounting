using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public class SessionHelper
    {
        private const string Session_DocumentDate = "DocumentDate";
        private const string Session_Calendar = "Calendar";
        private const string Session_Receipt = "Receipt";
        private const string Session_Bank = "Bank";
        private const string Session_PrecisionLimit = "PrecisionLimit";
        private const string Session_SOBId = "SOBId";
        private const string Session_BankAccount = "BankAccount";
        private const string Session_JV = "JV";
        private const string Session_Tax = "Tax";
        private const string Session_Invoice = "Invoice";
        private const string Session_Remittance = "Remittance";
        private const string SESSION_PAYMENT = "SESSION_Payment";

        public static ReceiptModel Receipt
        {
            get
            {
                return HttpContext.Current.Session[Session_Receipt] == null ? null :
                    (ReceiptModel)HttpContext.Current.Session[Session_Receipt];
            }
            set
            {
                HttpContext.Current.Session[Session_BankAccount] = value;
            }
        }

        public static BankAccountModel BankAccount
        {
            get
            {
                return HttpContext.Current.Session[Session_BankAccount] == null ? null :
                    (BankAccountModel)HttpContext.Current.Session[Session_BankAccount];
            }
            set
            {
                HttpContext.Current.Session[Session_BankAccount] = value;
            }
        }

        public static BankModel Bank
        {
            get
            {
                return HttpContext.Current.Session[Session_Bank] == null ? null :
                    (BankModel)HttpContext.Current.Session[Session_Bank];
            }
            set
            {
                HttpContext.Current.Session[Session_Bank] = value;
            }
        }

        public static RemittanceModel Remittance
        {
            get
            {
                return HttpContext.Current.Session[Session_Remittance] == null ? null :
                    (RemittanceModel)HttpContext.Current.Session[Session_Remittance];
            }
            set
            {
                HttpContext.Current.Session[Session_Remittance] = value;
            }
        }
        
        public static InvoiceModel Invoice
        {
            get
            {
            }
            set
            {
            }
        }

        public static TaxModel Tax
        {
            get
            {
            }
            set
            {
            }
        }

        public static GLHeaderModel JV
        {
            get
            {
            }
            set
            {
            }
        }

        public static PaymentHeaderModel Payment
        {
            get
            {
                return HttpContext.Current.Session[SESSION_PAYMENT] == null ? null :
                    (PaymentHeaderModel)HttpContext.Current.Session[SESSION_PAYMENT];
            }
            set
            {
                HttpContext.Current.Session[SESSION_PAYMENT] = value;
            }
        }

        public static DateTime DocumentDate
        {
            get
            {
                return Convert.ToDateTime(HttpContext.Current.Session[Session_DocumentDate].ToString());
            }
            set
            {
                HttpContext.Current.Session[Session_DocumentDate] = value;
            }
        }

        public static long SOBId
        {
            get
            {
            }
            set
            {
            }
        }

        public static long CodeCombinitionId
        {
            get
            {
                return Convert.ToInt64(HttpContext.Current.Session["CodeCombinitionId"].ToString());
            }
            set
            {
                HttpContext.Current.Session["CodeCombinitionId"] = value;
            }
        }

        public static long VendorId
        {
            get
            {
                return Convert.ToInt64(HttpContext.Current.Session["VendorId"].ToString());
            }
            set
            {
                HttpContext.Current.Session["VendorId"] = value;
            }
        }

        //TODO: this is temporary because of the issue 40 in task list.
        public static int PrecisionLimit
        {
            get
            {
            }
            set
            {
            }
        }

        //TODO: I am using it in JV to check the GL Date in GLHeader and Account in GL Lines..?
        public static CalendarViewModel Calendar
        {
            get
            {
            }
            set
            {
            }
        }

        
    }
}